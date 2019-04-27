#define WIN32_LEAN_AND_MEAN

#include <Windows.h>
#include <WinSock2.h>
#include <WS2tcpip.h>
#include <stdio.h>

#include <iostream>

#pragma comment (lib, "ws2_32.lib")

#define DEFAULT_PORT "27015"
#define DEFAULT_BUFLEN 512

int main()
{
	WSADATA wsaData;
	int iResult;
	int iSendResult;

	char recvbuf[DEFAULT_BUFLEN];
	int recvbufleng = DEFAULT_BUFLEN;

	SOCKET ClientSocket;
	ClientSocket = INVALID_SOCKET;

	struct addrinfo *result = NULL, hints;

	ZeroMemory(&hints, sizeof(hints));
	hints.ai_family = AF_INET;
	hints.ai_socktype = SOCK_STREAM;
	hints.ai_protocol = IPPROTO_TCP;
	hints.ai_flags = AI_PASSIVE;

	//initialize winsock
	iResult = WSAStartup(MAKEWORD(2, 2), &wsaData);
	if (iResult != 0)
	{
		printf("WSAStartup failed %d\n", iResult);
		system("pause");
		return 1;
	}

	iResult = getaddrinfo(NULL, DEFAULT_PORT, &hints, &result);
	if (iResult != 0) {
		printf("gettaddrinfo failed: %d\n", iResult);
		WSACleanup();
		system("pause");
		return 1;
	}

	SOCKET ListenSocket = INVALID_SOCKET;

	ListenSocket = socket(result->ai_family, result->ai_socktype, result->ai_protocol);

	if (ListenSocket == INVALID_SOCKET) {
		printf("error at socket(): %d\n", WSAGetLastError());
		freeaddrinfo(result);
		WSACleanup();
		system("pause");
		return 1;
	}

	iResult = bind(ListenSocket, result->ai_addr, (int)result->ai_addrlen);
	if (iResult == SOCKET_ERROR) {
		printf("bind failed with error: %d\n", WSAGetLastError());
		freeaddrinfo(result);
		closesocket(ListenSocket);
		WSACleanup();
		system("pause");
		return 1;
	}

	freeaddrinfo(result);

	if (listen(ListenSocket, SOMAXCONN) == SOCKET_ERROR) {
		printf("Listen failed with error: %d\n", WSAGetLastError());
		closesocket(ListenSocket);
		WSACleanup();
		system("pause");
		return 1;
	}

	printf("waiting for connection... \n");
	ClientSocket = accept(ListenSocket, NULL, NULL);
	printf("connection came through \n");
	if (ClientSocket == INVALID_SOCKET) {
		printf("accept failed with error", WSAGetLastError());
		closesocket(ListenSocket);
		WSACleanup();
		system("pause");
		return 1;
	}

	closesocket(ListenSocket);
	do {
		iResult = recv(ClientSocket, recvbuf, recvbufleng, 0);
		if (iResult > 0) {
			printf("Bytes received: %d\n", iResult);

			std::string message = recvbuf;
			message.shrink_to_fit();
			std::cout << message.c_str() << std::endl;
			printf("Connection established with client\n");
			iSendResult = send(ClientSocket, message.c_str(), message.length(), 0);
			if (iSendResult == SOCKET_ERROR) {
				printf("sendvfailed: %d\n", iSendResult);
				closesocket(ClientSocket);
				WSACleanup();
				system("pause");
				return 1;
			}
			printf("Bytes sent: &d\n", iSendResult);

			if (message.find("exit") == 0) {
				break;
			}
		}

		else if (iResult == 0) {

		}
			///printf("Connection closing... \n");
		else {
			printf("recv failed with error", WSAGetLastError());
			closesocket(ClientSocket);
			WSACleanup();
			system("pause");
			return 1;
		}
	} while (true);

	iResult = shutdown(ClientSocket, SD_SEND);
	if (iResult == SOCKET_ERROR) {
		printf("shutdown failed with error: %d\n", WSAGetLastError());
		freeaddrinfo(result);
		closesocket(ClientSocket);
		WSACleanup();
		system("pause");
		return 1;
	}

	closesocket(ClientSocket);
	WSACleanup();
	return 0;
}