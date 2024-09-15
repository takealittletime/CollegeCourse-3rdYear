import numpy as np
import random

def print_policy(pi, P, action_symbols=('<', 'v', '>', '^'), n_cols=4, title="정책:"):
    print(title)
    arrs = {k:v for k, v in enumerate(action_symbols)}
    for s in range(len(P)):
        a = pi(s)
        print('|', end="")
        if np.all([done for action in P[s].values() for _, _, _, done in action]):
            print("".rjust(9), end="")
        else:
            print(str(s).zfill(2), arrs[a].rjust(6), end=" ")

        if (s+1) % n_cols == 0:
            print("|")
            