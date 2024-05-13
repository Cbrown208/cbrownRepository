using System.Net;
using System.Net.Sockets;
using System.Text;

namespace PortSenderTool
{
	public class PortSenderManager
	{
		private PortSenderMessageManager _portSenderMessageManager = new PortSenderMessageManager();
		private static char END_OF_BLOCK = '\u001c';
		private static char START_OF_BLOCK = '\u000b';
		private static char CARRIAGE_RETURN = (char)13;
		public void SendMessages()
		{
			SendMessageToCdeNonProdTest();
			//SendMessageToCdeProdTest();
			//SendHl7MsgToMirthNonProd();

			//var response = SendMessageToTcpPortExample().Result;
			Console.WriteLine("Press Any key to Exit");
			Console.ReadLine();
		}

		private void SendMessageToCdeNonProdTest()
		{
			// Wake Forest Test Client
			var serverIp = "10.42.104.123";
			var serverPort = 42825;
			var message = _portSenderMessageManager.GetHl7TestMessage();
			SendHl7MsgToTcpPort(serverIp, serverPort, message);

		}

		private void SendMessageToCdeProdTest()
		{
			// nthrive Pas Test Client
			var serverIp = "10.42.104.124";
			var serverPort = 36215;
			var message = _portSenderMessageManager.GetNthriveProdTestClientHl7Message();

			SendHl7MsgToTcpPort(serverIp, serverPort, message);

		}

		private void SendHl7MsgToMirthNonProd()
		{
			// Mirth External IP
			var serverIp = "172.19.16.86";
			// Mirth nonprod accessable from Global VPN
			serverIp = "10.44.143.120";
			var serverPort = 19004;
			var message = _portSenderMessageManager.GetHl7TestMessage();

			SendHl7MsgToTcpPort(serverIp, serverPort, message);
		}

		private void SendHl7MsgToTcpPort(string serverIp, int serverPort, string message)
		{
			var testHl7MessageToTransmit = new StringBuilder();
			testHl7MessageToTransmit.Append(START_OF_BLOCK).Append(message).Append(END_OF_BLOCK).Append(CARRIAGE_RETURN);

			//---create a TCPClient object at the IP and port no.---
			TcpClient client = new TcpClient(serverIp, serverPort);
			NetworkStream nwStream = client.GetStream();
			byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(testHl7MessageToTransmit.ToString());

			//---send the text---
			Console.WriteLine("Sending : " + testHl7MessageToTransmit);
			nwStream.Write(bytesToSend, 0, bytesToSend.Length);

			//---read back the text---
			byte[] bytesToRead = new byte[client.ReceiveBufferSize];
			int bytesRead = nwStream.Read(bytesToRead, 0, client.ReceiveBufferSize);
			Console.WriteLine("Received : " + Encoding.ASCII.GetString(bytesToRead, 0, bytesRead));
			client.Close();
		}

		private async Task<bool> SendMessageToTcpPortExample()
		{
			//IPHostEntry ipHostInfo = await Dns.GetHostEntryAsync("google.com");
			IPHostEntry ipHostInfo = await Dns.GetHostEntryAsync("localhost");
			IPAddress ipAddress = ipHostInfo.AddressList[1];
			IPEndPoint ipEndPoint = new(ipAddress, 11_000);

			using Socket client = new(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

			await client.ConnectAsync(ipEndPoint);
			while (true)
			{
				// Send message.
				var message = "Hi friends 👋!<|EOM|>";
				var messageBytes = Encoding.UTF8.GetBytes(message);
				_ = await client.SendAsync(messageBytes, SocketFlags.None);
				Console.WriteLine($"Socket client sent message: \"{message}\"");

				// Receive ack.
				var buffer = new byte[1_024];
				var received = await client.ReceiveAsync(buffer, SocketFlags.None);
				var response = Encoding.UTF8.GetString(buffer, 0, received);
				if (response == "<|ACK|>")
				{
					Console.WriteLine(
						$"Socket client received acknowledgment: \"{response}\"");
					break;
				}
				// Sample output:
				//     Socket client sent message: "Hi friends 👋!<|EOM|>"
				//     Socket client received acknowledgment: "<|ACK|>"
			}

			client.Shutdown(SocketShutdown.Both);
			return true;
		}
	}
}
