#define WIN32_LEAN_AND_MEAN

#include <Windows.h>
#include <WinSock2.h>
#include <WS2tcpip.h>
#include <stdio.h>

#include <iostream>

#pragma comment (lib, "ws2_32.lib")

#define DEFAULT_PORT "27015"
#define DEFAULT_BUFLEN 512

int main(int argc, char **argv)
{
	WSADATA wsaData;
	int iResult, iSendResult;

	struct addrinfo *result = NULL,
		*ptr = NULL,
		hints;

	SOCKET ConnectSocket;
	ConnectSocket = INVALID_SOCKET;

	char sendbuf[] = "test data";
	char recvbuf[DEFAULT_BUFLEN];
	int recvbufleng = DEFAULT_BUFLEN;

	if (argc != 2) {
		printf("usage: %s server-name \n", argv[0]);
		system("pause");
		return 1;
	}

	iResult = WSAStartup(MAKEWORD(2, 2), &wsaData);
	if (iResult != 0) {
		printf("WSAStartup failed: %d\n", iResult);
		system("pause");
		return 1;
	}

	ZeroMemory(&hints, sizeof(hints));
	hints.ai_family = AF_UNSPEC;
	hints.ai_socktype = SOCK_STREAM;
	hints.ai_protocol = IPPROTO_TCP;

	iResult = getaddrinfo(argv[1], DEFAULT_PORT, &hints, &result);
	if (iResult != 0) {
		printf("getaddrinfo failed: %d\n", iResult);
		WSACleanup();
		system("pause");
		return 1;
	}

	for (ptr = result; ptr != NULL; ptr = ptr->ai_next) {
		ConnectSocket = socket(ptr->ai_family, ptr->ai_socktype, ptr->ai_protocol);
		if (ConnectSocket == INVALID_SOCKET) {
			printf("socket failed with error: ", WSAGetLastError);
			WSACleanup();
			system("pause");
			return 1;
		}

		iResult = connect(ConnectSocket, ptr->ai_addr, (int)ptr->ai_addrlen);
		if (iResult == SOCKET_ERROR) {
			closesocket(ConnectSocket);
			ConnectSocket = INVALID_SOCKET;
			continue;
		}

		break;
	}

	iResult = send(ConnectSocket, sendbuf, (int)strlen(sendbuf), 0);
	if (iResult == SOCKET_ERROR) {
		printf("sendvfailed: %d\n", iResult);
		closesocket(ConnectSocket);
		WSACleanup();
		system("pause");
		return 1;
	}

	printf("Bytes sent: %d\n", iResult);

	do {
		iResult = recv(ConnectSocket, recvbuf, recvbufleng, 0);
		if (iResult > 0) {
			printf("Bytes received: &d\n", iResult);
			std::string message = recvbuf;
			message.shrink_to_fit();
			std::cout << message.c_str() << std::endl;
			printf("Connection established with server\n");
			std::cin >> sendbuf;
			message = sendbuf;
			message.shrink_to_fit();
			iSendResult = send(ConnectSocket, message.c_str(), message.length(), 0);
			if (iSendResult == SOCKET_ERROR) {
				printf("sendvfailed: %d\n", iSendResult);
				closesocket(ConnectSocket);
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
			///printf("Connection closed\n");		
		}
			
		else
			printf("recv failed with error", WSAGetLastError());
	} while (true);

	iResult = shutdown(ConnectSocket, SD_SEND);
	if (iResult == SOCKET_ERROR) {
		printf("shutdown failed with error: %d\n", WSAGetLastError());
		closesocket(ConnectSocket);
		WSACleanup();
		system("pause");
		return 1;
	}

	closesocket(ConnectSocket);
	WSACleanup();
	printf("Closing connection...");
	system("pause");
	return 0;
}