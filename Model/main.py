# This is a sample Python script.

# Press ⌃R to execute it or replace it with your code.
# Press Double ⇧ to search everywhere for classes, files, tool windows, actions, and settings.
import numpy as np
import pandas as pd
import pycaret
from pycaret.classification import *

def print_hi(name):
    # Use a breakpoint in the code line below to debug your script.
    print(f'Hi, {name}')  # Press ⌘F8 to toggle the breakpoint.


# Press the green button in the gutter to run the script.
if __name__ == '__main__':
    d = {'Temperature': [50, 60, 80, 100, 40], 'Humidity': [23, 40, 60, 30, 10], 'Rained': [1, 1, 0, 0, 1]}
    for i in range(100):
        d['Temperature'].append(i + 10)
        d['Humidity'].append(i)
        d['Rained'].append(i % 2)
    print(d)
    df = pd.DataFrame(d)
    print(df.shape)
    exp_clf101 = setup(df, target='Rained', session_id=123)
    print(compare_models())
    dt = create_model('nb')
    print(dt)
    #print(df)

# See PyCharm help at https://www.jetbrains.com/help/pycharm/
