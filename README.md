![Banner](Assets/Banner.svg) 

## O que é o REST?
O que é uma API nós já sabemos. Mas o que faz com que ela passe a ser do "Tipo REST"?

O acrônimo REST se refere à _Representational State Transfer_ (Transferência de Estado Representacional) e podemos defini-lo como um tipo de _Arquitetura de Software_. Ou seja, uma API REST indica que um conjunto de padrões foram seguidos em seu desenvolvimento.

### Quais são essas restrições?
1. **Utilização de verbos apropriados:** Use os verbos HTTP apropriados para cada operação. Por exemplo, use GET para recuperar informações, POST para criar recursos, PUT para atualizar recursos e DELETE para remover recursos.
2. **Nomes de recursos significativos:** Escolha nomes de recursos que sejam descritivos e representativos do que eles representam. Evite usar verbos nos URLs.
3. **Use códigos de status HTTP apropriados:** Use códigos de status HTTP adequados para indicar o resultado de uma solicitação (por exemplo, 200 OK, 201 Created, 404 Not Found, 500 Internal Server Error).
4. **Respostas estruturadas:** Forneça respostas JSON ou XML estruturadas e bem formatadas para facilitar o processamento por parte dos clientes.
5. **Tratamento de erros consistente:** Implemente um sistema de tratamento de erros consistente que inclua mensagens de erro úteis e códigos de erro padronizados.
6. **Autenticação e autorização:** Proteja sua API com autenticação e autorização adequadas. Use padrões como OAuth 2.0 ou tokens JWT para autenticação.
7. **Documentação detalhada:** Forneça uma documentação completa e atualizada da API, incluindo exemplos de solicitações e respostas, parâmetros aceitáveis e descrições de recursos.
8. **Cache:** Utilize cabeçalhos de cache HTTP para melhorar o desempenho e reduzir a carga do servidor, quando apropriado.
9. **HATEOAS (Hypertext as the Engine of Application State):** Se necessário, inclua links em suas respostas que permitam aos clientes descobrir e navegar pelos recursos relacionados.
10.  **Versionamento da API:** nclua uma versão na URL da API (por exemplo, "/v1/users") para permitir atualizações futuras sem quebrar os clientes existentes.

## Descrição do Projeto

Nesse projeto de Treinamento, construi uma API RESTful que realiza operações no Banco de Dados relacionadas ao gerenciamento de Usuários. Nela temos as seguintes funcionalidades:

- Listagem de Usuários por Paginação;
- Busca de Usuário específico;
- Cadastro de Usuários;
- Atualização de Usuários;
- Exclusão de Usuários;
- Alteração do Status do Usuário (Ativo ou Inativo);
- Alteração do Tipo de Usuários (Nenhum, Comum, Desenvolvedor ou Administrador);
- Autenticação de usuário.

### Tecnologias Utilizadas
- .Net Core 7;
- Dapper;
- SQL Server;
- Criptografia AES;
- Autenticação com JWT Token.