# This is a sample Python script.
import json
from datetime import datetime

# Press ⌃R to execute it or replace it with your code.
# Press Double ⇧ to search everywhere for classes, files, tool windows, actions, and settings.
import numpy as np
import pandas as pd
import torch
import torch.nn as nn
import requests
from sklearn.preprocessing import MinMaxScaler
from flask import Flask, request, redirect
from flask_restful import Resource, Api
# from flask_cors import CORS
import os

app = Flask(__name__)
# cors = CORS(app, resources={r"*": {"origins": "*"}})
api = Api(app)
predicted_humidity = 101.99


def getPrediction():
    if predicted_humidity == 101.99:
        return "model not working"
    elif predicted_humidity <= 55:
        return f"predicted humidity is {predicted_humidity}: dry and comfortable"
    elif 55 < predicted_humidity < 65:
        return f"predicted humidity is {predicted_humidity}: becoming sticky"
    else:
        return f"predicted humidity is {predicted_humidity}: lots of moisture in the air"


# Press the green button in the gutter to run the script.
if __name__ == '__main__':
    url = "https://capstone-api-exlxxpyksq-uc.a.run.app/weatherforecast/getall"
    response = requests.get(url)
    response_json = response.json()
    with open("data.json", "w") as outfile:
        json.dump(response_json, outfile)
    df = pd.read_json("data.json", convert_dates=True)
    df = df.drop(columns=['id', 'temperature'])
    df['dates'] = df['dateTime'].apply(lambda x: x.toordinal())

    y = df['humidity'].values.astype(float)
    test_size = 12
    train_set = y[:-test_size]
    test_set = y[-test_size:]

    scaler = MinMaxScaler(feature_range=(-1, 1))

    train_norm = scaler.fit_transform(train_set.reshape(-1, 1))

    train_norm = torch.FloatTensor(train_norm).view(-1)

    window_size = 1


    def input_data(seq, ws):
        out = []
        L = len(seq)
        for i in range(L - ws):
            window = seq[i:i + ws]
            label = seq[i + ws:i + ws + 1]
            out.append((window, label))
        return out


    train_data = input_data(train_norm, window_size)
    print(len(train_data))
    print(train_data[0])


    class LSTMnetwork(nn.Module):
        def __init__(self, input_size=1, hidden_size=100, output_size=1):
            super().__init__()
            self.hidden_size = hidden_size

            self.lstm = nn.LSTM(input_size, hidden_size)

            self.linear = nn.Linear(hidden_size, output_size)

            self.hidden = (torch.zeros(1, 1, self.hidden_size), torch.zeros(1, 1, self.hidden_size))

        def forward(self, seq):
            lstm_out, self.hidden = self.lstm(
                seq.view(len(seq), 1, -1), self.hidden)
            pred = self.linear(lstm_out.view(len(seq), -1))
            return pred[-1]


    torch.manual_seed(42)

    model = LSTMnetwork()

    criterion = nn.MSELoss()

    optimizer = torch.optim.Adam(model.parameters(), lr=0.001)

    print(model)

    epochs = 100

    import time

    start_time = time.time()

    for epoch in range(epochs):
        for seq, y_train in train_data:
            optimizer.zero_grad()
            model.hidden = (torch.zeros(1, 1, model.hidden_size),
                            torch.zeros(1, 1, model.hidden_size))

            y_pred = model(seq)

            loss = criterion(y_pred, y_train)
            loss.backward()
            optimizer.step()

            print(f'Epoch: {epoch + 1:2} Loss: {loss.item():10.8f}')
    print(f'\nDuration: {time.time() - start_time:.0f} seconds')

    future = 1

    preds = train_norm[-window_size:].tolist()

    model.eval()

    for i in range(future):
        seq = torch.FloatTensor(preds[-window_size:])
        with torch.no_grad():
            model.hidden = (torch.zeros(1, 1, model.hidden_size),
                            torch.zeros(1, 1, model.hidden_size))
            preds.append(model(seq).item())
    print(preds[window_size:])

    true_predictions = scaler.inverse_transform(np.array(preds[window_size:]).reshape(-1, 1))
    # print(type(true_predictions[0][0]))
    predicted_humidity = true_predictions[0][0]
    print(predicted_humidity)

    value = getPrediction()

    print(value)

    print(type(value))

    data = {"pred": value}

    print(data)

    print(type(data))

    r = requests.post('https://capstone-api-exlxxpyksq-uc.a.run.app/prediction', json={"pred": value})

    print(r)
