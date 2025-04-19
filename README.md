# Azure Functions - Gerador e Validador de Boletos

![Azure Functions](https://img.shields.io/badge/Azure_Functions-0062AD?style=for-the-badge&logo=azure-functions&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)

Projeto com duas Azure Functions para geração (`fnGeradorBoleto`) e validação (`fnValidaBoleto`) de boletos bancários, além de um frontend para consumo das APIs.

## 📌 Funcionalidades

- **Geração de boletos** com código de barras e linha digitável
- **Validação de boletos** existentes
- **Frontend** para teste das APIs
- Suporte a execução local com Azure Functions Core Tools

## 🛠️ Tecnologias

- **Backend**:
  - Azure Functions (.NET 8)
  - Azure Storage (opcional para persistência)
  
- **Frontend**:
  - HTML5/CSS3
  - JavaScript

## 🚀 Como executar

### Pré-requisitos
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Azure Functions Core Tools](https://docs.microsoft.com/en-us/azure/azure-functions/functions-run-local)
- Node.js (para o frontend)
- Azure ServiceBus
- Newtonsoft.Json
- BarcodeStandard

## Passo a passo

1. Clone o repositório:
```bash
git clone https://github.com/oMaestro174/azure-cloud-native-lab005.git

```
2. Execute as Functions:
```bash
cd fnGeradorBoleto
func start --port 7292
```
3. Em outro terminal
```bash
cd fnValidaBoleto
func start --port 7220
```

4. Execute o frontend:

```bash
cd frontend
npm install
npm start
```

# 🌐 Endpoints
| Função           | Método | Endpoint                    | Descrição               |
|------------------|--------|-----------------------------|-------------------------|
| fnGeradorBoleto  | POST   | `/api/barcode-generate`     | Gera novo boleto        |
| fnValidaBoleto   | POST   | `/api/barcode-validate`     | Valida boleto existente |


#📄 Exemplos de Request/Response

## Geração de Boleto
Request:

```json
POST /api/barcode-generate 
{

"dataVencimento" : "2025-04-10",
"valor": "200"

}
```
Response:

```json
{
    "barcode": "1234567890",
    "valorOriginal": "100.00",
    "dataVencimento": "2025-04-24T19:23:42.1419907-04:00",
    "imagemBase64": "iVBORw0KGgoAAAANSUhEUgAAASwAAACWCAYAAABkW7XSAAAABHNCSVQICAgIfAhkiAAAAk9JREFUeJzt1EEKwjAUQMHE+985rgJFrFQE5eHMrs1vKKF9c621xkVzzjHGGGutMecc+9F9f689mz1b29ev9vxk36vX39j33dnHszmexavZs2c/fR+z/zn7+E0d148zZ2vb8R9/NnvF7a1pgB8SLCBDsIAMwQIyBAvIECwgQ7CADMECMgQLyBAsIEOwgAzBAjIEC8gQLCBDsIAMwQIyBAvIECwgQ7CADMECMgQLyBAsIEOwgAzBAjIEC8gQLCBDsIAMwQIyBAvIECwgQ7CADMECMgQLyBAsIEOwgAzBAjIEC8gQLCBDsIAMwQIyBAvIECwgQ7CADMECMgQLyBAsIEOwgAzBAjIEC8gQLCBDsIAMwQIyBAvIECwgQ7CADMECMgQLyBAsIEOwgAzBAjIEC8gQLCBDsIAMwQIyBAvIECwgQ7CADMECMgQLyBAsIEOwgAzBAjIEC8gQLCBDsIAMwQIyBAvIECwgQ7CADMECMgQLyBAsIEOwgAzBAjIEC8gQLCBDsIAMwQIyBAvIECwgQ7CADMECMgQLyBAsIEOwgAzBAjIEC8gQLCBDsIAMwQIyBAvIECwgQ7CADMECMgQLyBAsIEOwgAzBAjIEC8gQLCBDsIAMwQIyBAvIECwgQ7CADMECMgQLyBAsIEOwgAzBAjIEC8gQLCBDsIAMwQIyBAvIECwgQ7CADMECMgQLyBAsIEOwgAzBAjIEC8gQLCBDsIAMwQIyBAvIECwgQ7CADMECMgQLyBAsIEOwgAzBAjIEC8gQLCBDsIAMwQIyBAvIuAMS7+koQadzMQAAAABJRU5ErkJggg=="
}
```
Validação de Boleto
Request:

```json
POST /api/barcode-validate
{
  "barcode": "34191790010104351004791020150008785770000000150"
}

```
Response:

```json
{
    "valido": false,
    "mensagem": "O campo barcode deve ter 44 caracteres"
}
```
##🏗️ Estrutura do Projeto

```bash
📦 projeto-boletos
├── 📂 fnGeradorBoleto
│   ├── Function.cs
│   ├── host.json
│   ├── local.settings.json
│   └── Program.cs
├── 📂 fnValidaBoleto
│   ├── Function.cs
│   ├── host.json
│   ├── local.settings.json
│   └── Program.cs
└── 📂 frontend
    ├── index.html
    ├── app.js
    └── style.css

 ```   
## 📚 Documentação Adicional

[Azure Functions Documentation](https://docs.microsoft.com/en-us/azure/azure-functions/)

[Azure Functions Core Tools](https://docs.microsoft.com/en-us/azure/azure-functions/functions-run-local)


---


## 📸 Telas da aplicação e procedimentos

### Criacao da function gera boleto
![Tela da aplicação](/assets/02-criacao-da-function-gera-boleto.png)


### Instalação de dependências
![Tela da aplicação](/assets/03-instalacao-de-dependencias.png)

### Criando do Service Bus
![Tela da aplicação](/assets/04-criando-service-bus.png)

### Implementando a funcao gera codigo de barras
![Tela da aplicação](/assets/05-implementando-a-funcao-gera-codigo-de-barras.png)

### Testando com Postman
![Tela da aplicação](/assets/06-testando-com-postman.png)

### Verificando a Fila
![Tela da aplicação](/assets/07-verificando-fila.png)

### Verificando a fila detalhes
![Tela da aplicação](/assets/08-verificando-fila-detalhes.png)

### Verificando o corpo da mensagem
![Tela da aplicação](/assets/09-verificando-corpo-da-mensagem.png)

### Testando o barcode no postman
![Tela da aplicação](/assets/10-testando-barcode-no-postman.png)

### Converter base64 para imagem
![Tela da aplicação](/assets/11-converter-base64-para-imagem.png)

### Tela de front end html
![Tela da aplicação](/assets/12-criando-tela-de-front-para-gerar-boletos.png)

### Tela de front end web
![Tela da aplicação](/assets/12.1-criando-tela-de-front-para-gerar-boletos.png)

### Validando Boleto Function
![Tela da aplicação](/assets/13-valida-boletos.png)

### Validando Boleto Code
![Tela da aplicação](/assets/13.1-valida-boletos.png)

### Validando Boleto no Postman
![Tela da aplicação](/assets/13.3-valida-boletos-no-postman.png)

### Solution Visual Studio
![Tela da aplicação](/assets/13.2-tela-da-solution.png)


---



##



