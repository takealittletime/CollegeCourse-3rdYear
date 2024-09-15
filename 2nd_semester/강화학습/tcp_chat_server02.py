from socket import *
import threading

def handle_client(connectionSocket, addr):
    print('Connected by', addr)

    while True:
        message = connectionSocket.recv(1024).decode()
        if not message:
            break  # 클라이언트 연결이 끊어지면 루프를 종료
        print('Client:', message)

        reply = input('You: ')
        connectionSocket.send(reply.encode())

    connectionSocket.close()
    print('Connection closed by', addr)

serverPort = 12000
serverSocket = socket(AF_INET, SOCK_STREAM)
serverSocket.bind(('', serverPort))
serverSocket.listen(5)

print('The server is ready to receive')

while True:
    connectionSocket, addr = serverSocket.accept()

    # 각 클라이언트에 대해 새로운 스레드 생성
    client_handler = threading.Thread(target=handle_client, args=(connectionSocket, addr))
    client_handler.start()
