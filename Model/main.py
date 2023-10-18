# This is a sample Python script.

# Press ⌃R to execute it or replace it with your code.
# Press Double ⇧ to search everywhere for classes, files, tool windows, actions, and settings.
import numpy as np
import pandas as pd
import pycaret
from pycaret.classification import *
import requests

def print_hi(name):
    # Use a breakpoint in the code line below to debug your script.
    print(f'Hi, {name}')  # Press ⌘F8 to toggle the breakpoint.


# Press the green button in the gutter to run the script.
if __name__ == '__main__':
    df = pd.read_csv("historic_weather_data.csv")
    pd.set_option('display.max_columns', None)
    df = df['DailyAverageRelativeHumidity', 'DailyPrecipitation', ]
    print(df.columns.tolist())
    # token = "FNDXuekgrDWrywqFnFKfLuAQBYQdPgbV"
    # creds = dict(token=token)
    # dtype = 'locations'
    # url = "https://www.ncei.noaa.gov/cdo-web/api/v2/"
    # response = requests.get(url, dtype, headers=creds)
    # print(response.status_code)
    # print(response.json())

    # d = {'Temperature': [50, 60, 80, 100, 40], 'Humidity': [23, 40, 60, 30, 10], 'Rained': [1, 1, 0, 0, 1]}
    # for i in range(100):
    #     d['Temperature'].append(i + 10)
    #     d['Humidity'].append(i)
    #     d['Rained'].append(i % 2)
    # print(d)
    # df = pd.DataFrame(d)
    # print(df.shape)
    # exp_clf101 = setup(df, target='Rained', session_id=123)
    # print(compare_models())
    # dt = create_model('nb')
    # print(dt)
    #print(df)

# See PyCharm help at https://www.jetbrains.com/help/pycharm/
