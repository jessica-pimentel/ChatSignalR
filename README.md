# ChatBot com SignalR
Este projeto é um chat bot desenvolvido em C# utilizando a biblioteca SignalR para facilitar a comunicação em tempo real. 
O bot foi projetado para responder automaticamente às perguntas dos usuários em um ambiente de chat, proporcionando uma interação dinâmica e responsiva.

## Tecnologias Utilizadas
- **C#**: Linguagem de programação utilizada para o desenvolvimento do backend do chat bot.
- **SignalR**: Biblioteca para ASP.NET que permite comunicação em tempo real através de HTTPs. SignalR é usado neste projeto para gerenciar as conexões e a troca de mensagens entre o servidor e os clientes.

## Client x Server
- Criação do lado do Client para escutar hub será feito com Html, CSS, JS e React.
- Criação do lado do Server que se conecta e envia mensagem ao hub com C#.

## Funcionalidades
- **Conexão em tempo real**: Utilizando SignalR, o chat bot mantém uma conexão persistente com o cliente, permitindo interações instantâneas entre client e server.
- **Respostas Automáticas**: O bot é capaz de interpretar as mensagens recebidas e fornecer respostas automáticas baseadas em escuta do lado do client.
