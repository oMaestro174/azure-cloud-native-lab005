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
  "valor": 150.75,
  "vencimento": "2025-12-31",
  "beneficiario": "Empresa XYZ",
  "pagador": "Cliente ABC"
}
```
Response:

```json
{
  "codigoBarras": "34191790010104351004791020150008785770000000150",
  "linhaDigitavel": "34191.09102 20150.008785 57700.000001 1 79001010435",
  "pdfBase64": "JVBERi0xLjQK..."
}
```
Validação de Boleto
Request:

```json
POST /api/barcode-validate
{
  "codigoBarras": "34191790010104351004791020150008785770000000150"
}

```
Response:

```json
{
  "valido": true,
  "detalhes": {
    "valor": 150.75,
    "vencimento": "2025-12-31",
    "beneficiario": "Empresa XYZ"
  }
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

### Codificação de API
![Tela da aplicação](assets/01-criando-a-api.png)


### Publicação da API
![Tela da aplicação](assets/02-publicacao-da-api.png)

### Um pequeno erro,ops!
![Tela da aplicação](assets/03-erro-deploy-do-apim.png)

### Criação do apim concluída
![Tela da aplicação](assets/04-criacao-do-apim-concluida.png)

### API acessível publicamente
![Tela da aplicação](assets/05-api-acessivel-publicamente.png)

### Testes da API via Postman
![Tela da aplicação](assets/06-testes-da-api-via-postman.png)

### Alterando e Configurando
![Tela da aplicação](assets/07-alterando-e-configurando-acesso-a-api.png)

### Limitando Acesso via CORS
![Tela da aplicação](assets/08-limitando-acesso-a-api-via-cors.png)

### Alterando Políticas de Inbound
![Tela da aplicação](assets/09-alterando-politicas-inbound.png)

### Testando as Limitações de Permissões
![Tela da aplicação](assets/10-testando-as-limitacoes-de-permissoes.png)

---



##



