from socket import *

serverName = '127.0.0.1'
serverPort = 12000
clientSocket = socket(AF_INET, SOCK_STREAM)
clientSocket.connect((serverName, serverPort))

print('Connected to server')

while True:
    # 사용자로부터 메시지를 입력하고 서버에게 보냄
    message = input('You: ')
    clientSocket.send(message.encode())

    # 서버로부터 응답을 받아 출력
    reply = clientSocket.recv(1024).decode()
    print('Server:', reply)

# 클라이언트 소켓을 닫음
clientSocket.close()
