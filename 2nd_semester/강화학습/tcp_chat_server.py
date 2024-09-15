from socket import *

serverPort = 12000
serverSocket = socket(AF_INET, SOCK_STREAM)
serverSocket.bind(('', serverPort))
serverSocket.listen(5)

print('The server is ready to receive')

while True:
    # 클라이언트의 연결을 기다림
    connectionSocket, addr = serverSocket.accept()

    print('Connected by', addr)

    while True:
        # 클라이언트로부터 메시지를 받음
        message = connectionSocket.recv(1024).decode()
        print('Client:', message)

        # 서버에서 메시지를 입력하고 클라이언트에게 보냄
        reply = input('You: ')
        connectionSocket.send(reply.encode())

    # 클라이언트 소켓을 닫음
    connectionSocket.close()
