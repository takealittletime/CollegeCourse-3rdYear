import time
import numpy as np
import tkinter as tk
from PIL import ImageTk, Image
#20194111 최민규
#필요한 라이브러리를 import.
import torch
import torch.nn as nn
import torch.nn.functional as F
import torch.optim as optim
import random
from tqdm import *

PhotoImage = ImageTk.PhotoImage
UNIT = 50  # 픽셀 수
HEIGHT = 5  # 그리드 세로
WIDTH = 5  # 그리드 가로

np.random.seed(1)
#ㅔㅛ쇄ㅜ --

class Env(tk.Tk):
    def __init__(self, name, render_speed=0.01):
        super(Env, self).__init__()
        self.render_speed=render_speed
        self.action_space = ['u', 'd', 'l', 'r']
        self.action_size = len(self.action_space)
        self.title(name)
        self.geometry('{0}x{1}'.format(HEIGHT * UNIT, HEIGHT * UNIT))
        self.shapes = self.load_images()
        self.canvas = self._build_canvas()
        self.counter = 0
        self.rewards = []  # 장애물, 목표지점을 나타내는 딕셔너리의 리스트
                           # 딕셔너리의 키: reward, state, direction, coords, figure
        self.goal = []
        # 장애물 설정
        self.set_reward([0, 1], -1)
        self.set_reward([1, 2], -1)
        self.set_reward([2, 3], -1)
        # 목표 지점 설정
        self.set_reward([4, 4], 1)

    def _build_canvas(self):
        canvas = tk.Canvas(self, bg='white',
                           height=HEIGHT * UNIT,
                           width=WIDTH * UNIT)
        # 그리드 생성
        for c in range(0, WIDTH * UNIT, UNIT):  # 0~400 by 80
            x0, y0, x1, y1 = c, 0, c, HEIGHT * UNIT
            canvas.create_line(x0, y0, x1, y1)
        for r in range(0, HEIGHT * UNIT, UNIT):  # 0~400 by 80
            x0, y0, x1, y1 = 0, r, HEIGHT * UNIT, r
            canvas.create_line(x0, y0, x1, y1)

        self.rewards = []
        self.goal = []
        # 캔버스에 이미지 추가
        x, y = UNIT/2, UNIT/2
        self.rectangle = canvas.create_image(x, y, image=self.shapes[0])

        canvas.pack()                

        return canvas

    def load_images(self):
        rectangle = PhotoImage(
            Image.open("img/rectangle.png").resize((30, 30)))
        triangle = PhotoImage(
            Image.open("img/triangle.png").resize((30, 30)))
        circle = PhotoImage(
            Image.open("img/circle.png").resize((30, 30)))

        return rectangle, triangle, circle

    def reset_reward(self):

        for reward in self.rewards:
            self.canvas.delete(reward['figure'])

        self.rewards.clear()
        self.goal.clear()
        self.set_reward([0, 1], -1)
        self.set_reward([1, 2], -1)
        self.set_reward([2, 3], -1)

        # #goal
        self.set_reward([4, 4], 1)

    # 각 상태에 대해 reward 딕셔너리 생성
    def set_reward(self, state, reward):
        state = [int(state[0]), int(state[1])]
        x = int(state[0])
        y = int(state[1])
        temp = {}
        if reward > 0:
            temp['reward'] = reward
            temp['figure'] = self.canvas.create_image((UNIT * x) + UNIT / 2,
                                                       (UNIT * y) + UNIT / 2,
                                                       image=self.shapes[2])

            self.goal.append(temp['figure'])


        elif reward < 0:
            temp['direction'] = -1
            temp['reward'] = reward
            temp['figure'] = self.canvas.create_image((UNIT * x) + UNIT / 2,
                                                      (UNIT * y) + UNIT / 2,
                                                      image=self.shapes[1])

        temp['coords'] = self.canvas.coords(temp['figure'])
        temp['state'] = state
        self.rewards.append(temp)

    # 에이전트가 장애물, 목표지점 도달 시 체크 리스트(딕셔너리) 리턴
    def check_if_reward(self, state):
        check_list = dict()
        check_list['if_goal'] = False
        rewards = 0

        for reward in self.rewards:
            if reward['state'] == state:
                rewards += reward['reward']       # 장애물 충돌 시 마다 보상 -1 누적
                if reward['reward'] == 1:
                    check_list['if_goal'] = True

        check_list['rewards'] = rewards

        return check_list

    def coords_to_state(self, coords):
        x = int((coords[0] - UNIT / 2) / UNIT)
        y = int((coords[1] - UNIT / 2) / UNIT)
        return [x, y]

    def reset(self):
        self.update()
        time.sleep(0.5)
        x, y = self.canvas.coords(self.rectangle)
        self.canvas.move(self.rectangle, UNIT / 2 - x, UNIT / 2 - y)
        self.reset_reward()
        return self.get_state()

    # 한 스텝 진행
    def step(self, action):
        self.counter += 1
        self.render()
        
        # 2 스텝에 한 번씩 장애물 이동
        if self.counter % 2 == 1:
            self.rewards = self.move_rewards()    # 장애물 이동 후의 reward 계산

        # 에이전트 이동
        next_coords = self.move(self.rectangle, action)
        # 장애물 충돌 체크
        check = self.check_if_reward(self.coords_to_state(next_coords))
        done = check['if_goal']      # 목표지점 도달 시
        reward = check['rewards']    # 중복 장애물 충돌 시누적 보상

        self.canvas.tag_raise(self.rectangle)

        s_ = self.get_state()    # 에이전트의 도착지점, 장애물에 대한 상대 위치, 라벨, 이동 방향

        return s_, reward, done

    # 에이전트 이동 후 상태(상대 위치, 라벨) 등
    def get_state(self):

        location = self.coords_to_state(self.canvas.coords(self.rectangle))
        agent_x = location[0]
        agent_y = location[1]

        states = list()

        for reward in self.rewards:
            reward_location = reward['state']
            states.append(reward_location[0] - agent_x)
            states.append(reward_location[1] - agent_y)
            # 장애물이면
            if reward['reward'] < 0:
                states.append(-1)    # 장애물 표시 라벨
                states.append(reward['direction'])   # 장애물의 이동 방향
            else:
                states.append(1)   # 목표 지점 라벨

        return states

    # 장애물 이동, 이동 후 변경된 위치의 reward 딕셔너리 리스트 리턴
    def move_rewards(self):        
        new_rewards = []
        for temp in self.rewards:
            if temp['reward'] == 1:       # 목표 지점이면
                new_rewards.append(temp)
                continue
            temp['coords'] = self.move_const(temp)   # 이동 후 좌표
            temp['state'] = self.coords_to_state(temp['coords'])  # 이동 후 위치
            new_rewards.append(temp)
        return new_rewards

    # 이동 후 좌표 리턴
    def move_const(self, target):

        s = self.canvas.coords(target['figure'])

        base_action = np.array([0, 0])

        # 오른 쪽 끝이면 
        if s[0] == (WIDTH - 1) * UNIT + UNIT / 2:
            target['direction'] = 1      # 왼쪽 방향 이동
        # 왼쪽 끝이면
        elif s[0] == UNIT / 2:          # 오른쪽 이동
            target['direction'] = -1

        # 오른쪽 이동이면 픽셀 좌표 증가
        if target['direction'] == -1:
            base_action[0] += UNIT
        # 왼쪽 이동이면 픽셀 좌표 감소
        elif target['direction'] == 1:
            base_action[0] -= UNIT
        # ? rectangle
        if (target['figure'] is not self.rectangle
           and s == [(WIDTH - 1) * UNIT, (HEIGHT - 1) * UNIT]):
            base_action = np.array([0, 0])

        self.canvas.move(target['figure'], base_action[0], base_action[1])

        s_ = self.canvas.coords(target['figure'])

        return s_

    def move(self, target, action):
        s = self.canvas.coords(target)

        base_action = np.array([0, 0])

        if action == 0:  # 상
            if s[1] > UNIT:
                base_action[1] -= UNIT
        elif action == 1:  # 하
            if s[1] < (HEIGHT - 1) * UNIT:
                base_action[1] += UNIT
        elif action == 2:  # 우
            if s[0] < (WIDTH - 1) * UNIT:
                base_action[0] += UNIT
        elif action == 3:  # 좌
            if s[0] > UNIT:
                base_action[0] -= UNIT

        self.canvas.move(target, base_action[0], base_action[1])

        s_ = self.canvas.coords(target)

        return s_

    def render(self):
        # 게임 속도 조정
        time.sleep(self.render_speed)
        self.update()
        
class FCQ(nn.Module):
    def __init__(self, input_dim, hidden_dims=(32, 32), activation_fc=F.relu):
        # nn.Module을 상속받아 FCQ 클래스를 정의하는 생성자 메서드
        super(FCQ, self).__init__()

        # 활성화 함수, 입력 레이어, 은닉 레이어, 출력 레이어를 정의하는 부분
        self.activation_fc = activation_fc

        # 입력 레이어 정의
        self.input_layer = nn.Linear(input_dim, hidden_dims[0])

        # 은닉 레이어들을 ModuleList로 정의
        self.hidden_layers = nn.ModuleList()
        for i in range(len(hidden_dims) - 1):
            hidden_layer = nn.Linear(hidden_dims[i], hidden_dims[i + 1])
            self.hidden_layers.append(hidden_layer)

        # 출력 레이어 정의
        self.output_layer = nn.Linear(hidden_dims[-1], 1)  # Q-value는 하나의 숫자이므로 출력 레이어의 뉴런 수는 1

        # CUDA를 사용할 수 있는 경우 GPU로 모델을 이동
        device = "cpu"
        if torch.cuda.is_available():
            device = "cuda:0"
        self.device = torch.device(device)
        self.to(self.device)

    def _format(self, state):
        # 입력 상태를 PyTorch Tensor로 변환하고, 필요에 따라 차원을 추가하는 보조 함수
        x = state
        if not isinstance(x, torch.Tensor):
            x = torch.tensor(x, device=self.device, dtype=torch.float32)
            x = x.unsqueeze(0)
        return x

    def forward(self, state):
        # 전달된 상태에 대한 Q-value를 계산하는 forward 메서드
        x = self._format(state)
        x = self.activation_fc(self.input_layer(x))
        
        # 숨겨진 레이어들을 통과
        for hidden_layer in self.hidden_layers:
            x = self.activation_fc(hidden_layer(x))
        
        # 최종 출력 레이어를 통과하여 Q-value 예측
        x = self.output_layer(x)
        return x
def ToList(d):
    # 주어진 데이터를 리스트로 변환하는 함수
    t = []
    t.append(d)
    return t

class DeepSarsa():
    def __init__(self, value_model_fn, value_optimizer_fn, value_optimizer_lr, epsilon=0.1):
        # DeepSarsa 클래스의 생성자 메서드
        self.value_model_fn = value_model_fn  # Q-value 모델 생성 함수
        self.value_optimizer_fn = value_optimizer_fn  # Q-value 모델 최적화 함수
        self.value_optimizer_lr = value_optimizer_lr  # Q-value 모델 최적화 학습률
        self.epsilon = epsilon  # 입실론-그리디 정책에서의 입실론 값 (탐험 확률)

    def select_action(self, state):
        # 주어진 상태에 대한 행동을 선택하는 메서드
        with torch.no_grad():
            q_values = self.online_model(state).cpu().detach().data.numpy().squeeze()
        #print("q_values:",q_values)
        if np.random.rand() > self.epsilon:
            action = np.argmax(q_values)  # 입실론-그리디 정책을 따라 행동 선택
        else:
            action = np.random.randint(len(q_values))  # 무작위로 행동 선택
        return action

    def optimize_model(self, experiences):
        # Q-value 모델을 최적화하는 메서드
        states, actions, rewards, next_states, is_terminals = experiences

        # 현재 상태에서의 Q-value 계산
        q_sa = self.online_model(states).max(1)[0]

        # 다음 상태에서의 행동 선택 및 해당 Q-value 계산
        next_action = self.select_action(next_states)
        q_next_sa = self.online_model(next_states)[0][next_action]

        # 타겟 Q-value 계산
        target_q_s = rewards + self.gamma * q_next_sa * (1 - is_terminals)

        # Temporal Difference 오류 및 값 손실 계산
        td_errors = q_sa - target_q_s
        value_loss = td_errors.pow(2).mul(0.5).mean()

        # Q-value 모델 최적화
        self.value_optimizer.zero_grad()
        value_loss.backward()
        self.value_optimizer.step()
    def train(self, env, gamma, max_episodes):
        # 훈련을 위한 메서드
        nS = 15
        nA = 4
        self.gamma = gamma
        min_epsilon = 0.1
        epsilon_decay_ratio = 0.9
        self.episode_reward = []  # 에피소드별 보상을 저장하는 리스트 초기화
        self.online_model = self.value_model_fn(nS, nA)  # Q-value 모델 초기화
        self.value_optimizer = self.value_optimizer_fn(self.online_model, self.value_optimizer_lr)  # Q-value 모델 최적화기 초기화

        # 주어진 에피소드 수만큼 반복
        for e in tqdm(range(max_episodes), desc='All Episodes'):
            state, is_terminal = env.reset(), False
            self.episode_reward.append(0)  # 현재 에피소드의 보상 초기화

            # 에피소드 진행
            while not is_terminal:
                action = self.select_action(state)  # 현재 상태에서 행동 선택
                new_state, reward, is_terminal = env.step(action)  # 행동 수행 및 다음 상태, 보상, 종료 여부 획득
                self.episode_reward[-1] += reward  # 현재 에피소드의 총 보상 갱신

                # 텐서로 상태, 행동, 보상, 다음 상태, 종료 여부를 변환하여 저장
                s = torch.Tensor(state).unsqueeze(0)
                a = torch.Tensor(ToList(action)).unsqueeze(0)
                r = torch.Tensor(ToList(reward)).unsqueeze(0)
                ns = torch.Tensor(new_state).unsqueeze(0)
                done = torch.Tensor(ToList(float(is_terminal))).unsqueeze(0)
                experience = (s, a, r, ns, done)

                self.optimize_model(experience)  # 모델 최적화 수행

                state = new_state  # 상태 업데이트

            if self.epsilon > min_epsilon:
                self.epsilon *= epsilon_decay_ratio  # 입실론 감소

        return self.episode_reward  # 모든 에피소드의 보상을 반환

    
# 환경 설정
environment_settings = {
    'env_name': '그리드 월드',
    'gamma': 0.9,
    'max_episodes': 500
}

# Q-value 모델을 생성하는 함수 정의
value_model_fn = lambda nS, nA: FCQ(input_dim=nS, hidden_dims=(30,), activation_fc=F.relu)

# Q-value 모델을 최적화하는 함수 정의
value_optimizer_fn = lambda net, lr: optim.RMSprop(net.parameters(), lr=lr)

# Q-value 모델의 최적화 학습률 설정
value_optimizer_lr = 0.001

# 환경 설정 변수 가져오기
env_name, gamma, max_episodes = environment_settings.values()

# 환경 생성
env = Env('DeepSarsa')

# DeepSarsa 에이전트 생성
agent = DeepSarsa(value_model_fn, value_optimizer_fn, value_optimizer_lr)

# 훈련 수행 및 보상 기록
reward_track = agent.train(env, gamma, max_episodes)

# 시각화를 위한 Matplotlib 설정
import matplotlib.pyplot as plt
import matplotlib.pylab as pylab

plt.style.use('fivethirtyeight')
params = {
    'figure.figsize': (15, 8),
    'font.size': 24,
    'legend.fontsize': 20,
    'axes.titlesize': 28,
    'axes.labelsize': 24,
    'xtick.labelsize': 20,
    'ytick.labelsize': 20,
}
pylab.rcParams.update(params)

# 보상 추이 시각화
plt.plot(reward_track)
plt.title('Deep Sarsa: Grid World')
plt.xlabel('episodes')
plt.ylabel('rewards')
plt.show()
