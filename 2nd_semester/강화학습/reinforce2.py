import numpy as np
import random
import gym


def policy_evaluation(pi, P, gamma=1.0, theta=1e-10):
    prev_V = np.zeros(len(P), dtype=np.float64)

    while True:
        V = np.zeros(len(P), dtype=np.float64)
        for s in range(len(P)):
            for prob, next_state, reward, done in P[s][pi(s)]:
                V[s] += prob * (reward + gamma * prev_V[next_state] * (not done))

        if np.max(np.abs(prev_V - V)) < theta:
            break
        prev_V = V.copy()

    return V

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

def probability_success(env, pi, goal_state, n_episodes=100, max_steps=200):
    random.seed(123)
    np.random.seed(123)

    results = []
    for _ in range(n_episodes):
        s, done, steps = env.reset(seed=123), False, 0
        state = s[0]
        while not done and steps < max_steps:
            state, _, done, _, _ = env.step(pi(state))
            steps += 1
        results.append(state == goal_state)

    return np.sum(results) / len(results)

def mean_return(env, pi, n_episodes=100, max_steps=200):
    random.seed(123)
    np.random.seed(123)

    results = []

    for _ in range(n_episodes):
        s, done, steps = env.reset(seed=123), False, 0
        state = s[0]
        results.append(0.0)
        while not done and steps < max_steps:
            state, reward, done, _, _ = env.step(pi(state))
            results[-1] += reward
            steps += 1

    return np.mean(results)

def print_state_value_function(V, P, n_cols=4, prec=3, title="상태-가치 함수:"):
    print(title)
    for s in range(len(P)):
        v =V[s]
        print("|", end="")
        if np.all([done for action in P[s].values() for _, _, _, done in action]):
            print("".rjust(9), end=" ")
        else:
            print(str(s).zfill(2), f'{np.round(v, prec)}'.rjust(6), end=" ")
        if (s+1) % n_cols == 0:
            print("|")


def policy_improvement(V, P, gamma=1.0):
    Q = np.zeros((len(P), len(P[0])), dtype=np.float64)
    for s in range(len(P)):
        for a in range(len(P[s])):
            for prob, next_state, reward, done in P[s][a]:
                Q[s][a] += prob * (reward + gamma * V[next_state] * (not done))

    new_pi = lambda s: {s: a for s, a in enumerate(np.argmax(Q, axis=1))}[s]

    return new_pi


env = gym.make("FrozenLake-v1")
P = env.env.P

init_state = env.reset(seed=123)
goal_state = 15

random_policy = {}

for i in range(16):
    random_policy[i] = np.random.choice(4)

# random_pi = lambda s: random_policy[s]
random_pi = lambda s: {
    0:2, 1:0, 2:1, 3:3,
    4:0, 5:0, 6:2, 7:0,
    8:3, 9:1, 10:3, 11:0,
    12:0, 13:2, 14:1, 15:0 }[s]

print_policy(random_pi, P)
print(f'성공확률 {probability_success(env, random_pi, goal_state=goal_state)*100:.2f} 평균리턴값: {mean_return(env, random_pi):.4f}')

print("")
V = policy_evaluation(random_pi, P, gamma=0.99)
print_state_value_function(V, P, prec=4)