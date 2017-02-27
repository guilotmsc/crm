CREATE TABLE Endereco (
id INTEGER PRIMARY KEY,
numero VARCHAR(10),
bairro VARCHAR(100),
rua VARCHAR(50),
complemento VARCHAR(50),
cep VARCHAR(15),
tipo VARCHAR(30)
);

CREATE TABLE Estacao (
id INTEGER PRIMARY KEY,
nome VARCHAR(200),
data_criacao TIMESTAMP
);

CREATE TABLE Cliente (
id INTEGER PRIMARY KEY,
estacao_id INTEGER,
endereco_id INTEGER,
func_alteracao INTEGER,
email VARCHAR(200),
nome VARCHAR(100),
telefone VARCHAR(30),
data_criacao TIMESTAMP,
data_alteracao TIMESTAMP,
ativo bool,
FOREIGN KEY(estacao_id) REFERENCES Estacao (id),
FOREIGN KEY(endereco_id) REFERENCES Endereco (id)
);

CREATE TABLE Pessoa (
id INTEGER PRIMARY KEY,
cliente_id INTEGER,
pessoa_cpf VARCHAR(12),
nome VARCHAR(200),
pessoa_sexo VARCHAR(10),
pessoa_rg VARCHAR(12),
pessoa_estado_civil VARCHAR(12),
e_func bool,
FOREIGN KEY(cliente_id) REFERENCES Cliente (id)
);

CREATE TABLE Negocio (
id INTEGER PRIMARY KEY,
estacao_id INTEGER,
organizacao_id INTEGER,
cliente_id INTEGER,
func_alteracao INTEGER,
func_criacao INTEGER,
funil_id INTEGER,
data_criacao TIMESTAMP,
data_alteracao TIMESTAMP,
data_fechamento TIMESTAMP,
descricao text,
valor NUMERIC,
FOREIGN KEY(estacao_id) REFERENCES Estacao (id),
FOREIGN KEY(cliente_id) REFERENCES Pessoa (id),
FOREIGN KEY(func_alteracao) REFERENCES Pessoa (id),
FOREIGN KEY(func_criacao) REFERENCES Pessoa (id)
);

CREATE TABLE Organizacao (
id INTEGER PRIMARY KEY,
cliente_id INTEGER,
cnpj VARCHAR(20),
inscricao_estadual VARCHAR(30),
razao_social VARCHAR(100),
e_fornecedor bool,
FOREIGN KEY(cliente_id) REFERENCES Cliente (id)
);

CREATE TABLE participantes (
negocio_id INTEGER,
cliente_id INTEGER,
PRIMARY KEY(negocio_id,cliente_id),
FOREIGN KEY(negocio_id) REFERENCES Negocio (id),
FOREIGN KEY(cliente_id) REFERENCES Pessoa (id)
);

CREATE TABLE Funil (
id INTEGER PRIMARY KEY,
func_criacao INTEGER,
func_alteracao INTEGER,
data_alteracao TIMESTAMP,
data_criacao TIMESTAMP,
descricao VARCHAR(150),
nivel INTEGER,
FOREIGN KEY(func_criacao) REFERENCES Pessoa (id),
FOREIGN KEY(func_alteracao) REFERENCES Pessoa (id)
);

CREATE TABLE Negocio_Nota (
id INTEGER PRIMARY KEY,
negocio_id INTEGER,
func_criacao INTEGER,
descricao text,
FOREIGN KEY(negocio_id) REFERENCES Negocio (id),
FOREIGN KEY(func_criacao) REFERENCES Pessoa (id)
);

CREATE TABLE Atividade (
id INTEGER PRIMARY KEY,
tipo_atividade_id INTEGER,
negocio_id INTEGER,
organizacao_id INTEGER,
cliente_id INTEGER,
func_criacao INTEGER,
data_atividade TIMESTAMP,
data_criacao TIMESTAMP,
data_alteracao TIMESTAMP,
func_alteracao INTEGER,
estacao_id INTEGER,
FOREIGN KEY(negocio_id) REFERENCES Negocio (id),
FOREIGN KEY(organizacao_id) REFERENCES Organizacao (id),
FOREIGN KEY(cliente_id) REFERENCES Pessoa (id),
FOREIGN KEY(func_criacao) REFERENCES Pessoa (id),
FOREIGN KEY(func_alteracao) REFERENCES Pessoa (id),
FOREIGN KEY(estacao_id) REFERENCES Estacao (id)
);

CREATE TABLE tipo_atividade (
id INTEGER PRIMARY KEY,
func_criacao INTEGER,
func_alteracao INTEGER,
nome VARCHAR(150),
data_criacao TIMESTAMP,
data_alteracao TIMESTAMP,
FOREIGN KEY(func_criacao) REFERENCES Pessoa (id),
FOREIGN KEY(func_alteracao) REFERENCES Pessoa (id)
);

CREATE TABLE Atividade_Nota (
id INTEGER PRIMARY KEY,
atividade_id INTEGER,
func_criacao INTEGER,
descricao text,
FOREIGN KEY(atividade_id) REFERENCES Atividade (id),
FOREIGN KEY(func_criacao) REFERENCES Pessoa (id)
);

ALTER TABLE Cliente ADD FOREIGN KEY(func_alteracao) REFERENCES Pessoa (id);
ALTER TABLE Negocio ADD FOREIGN KEY(organizacao_id) REFERENCES Organizacao (id);
ALTER TABLE Negocio ADD FOREIGN KEY(funil_id) REFERENCES Funil (id);
ALTER TABLE Atividade ADD FOREIGN KEY(tipo_atividade_id) REFERENCES tipo_atividade (id);
