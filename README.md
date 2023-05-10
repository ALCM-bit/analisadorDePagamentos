# Analisador de Pagamentos

## Descrição

Esse projeto foi desenvolvido como um desafio para ser entregue para a empresa Auvo Tecnologia. O desafio consistiu em criar uma webapi que recebesse as informações de arquivos csv em uma pasta especificada solicitada pelo usuário, e retornasse um arquivo Json com o resultado. A parte mais complexa foi garantir uma boa abstração e manipular as informações para gerar o resultado desejado e aplicar o assincronismo. 
Com este projeto pude aprender sobre assincronismo, entendendo um pouco melhor como aplicalo , além de conhecer o CsvHelper uma biblioteca para manusear csv.

### Sobre o uso
1. Os arquivos csv na pasta devem estar no seguinte formato:
- Nome do arquivo conter: Nome do Departamento, Mês de vigência, Ano de vigência. Exemplo: ‘Departamento de Operações Especiais-Abril-2022.csv’
- O arquivo deve conter as seguintes colunas: Código: número, Nome: Texto, Valorhora: Dinheiro, Data: Dia do registro, Entrada: Hora do registro, Saída: Hora do registro, Almoço: Hora de registro
```
Código;Nome;Valorhora;Data;Entrada;Saída;Almoço
1;João da Silva;R$ 110, 97;01/04/2022;08:00:00;18:00:00;12:00 - 13:00
1;João da Silva;R$ 110, 97;02/04/2022;08:00:00;18:00:00;12:00 - 13:00
1;João da Silva;R$ 110, 97;03/04/2022;08:00:00;18:00:00;12:00 - 13:00
1;João da Silva;R$ 110, 97;04/04/2022;08:00:00;18:00:00;12:00 - 13:00
```
2. No final além do retorno da api, será gerado um arquivo chamado result.json no seguinte formato:
```
[
  {
    "Nome": "Departamento de Desenvolvimento",
    "MesVigencia": "Abril",
    "AnoVigencia": "2022",
    "TotalPagar": 29961.90,
    "TotalDescontos": 0.00,
    "TotalExtras": 3745.23750,
    "Funcionarios": [
      {
        "Codigo": 1,
        "Nome": "João da Silva",
        "TotalReceber": 29961.90,
        "HorasExtra": 30.0,
        "HorasDebito": 0.0,
        "DiasFalta": 0,
        "DiasExtras": 10,
        "DiasTrabalhados": 30
      }
]
```

## Tecnologias Utilizadas
- Linguagem C#
- IDE Visual Studio
- POO (Programação Orientada a Objetos)
- ASP.NET


