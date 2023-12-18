using System;
using System.Collections.Generic;

class Filial : Empresa
{
    public string cnpj { get; protected set; }
    public int quantidade_funcionarios { get; set; }
    public string filial_estado { get; protected set; }


    // Construtor
    public Filial(string nome, DateTime data_criacao, int identificacao, string cnpj, int quantidade_funcionarios, string filial_estado) : base(nome, data_criacao, identificacao, "65.777753/0001-47")
    {
        this.cnpj = "65.777753/0001-" + cnpj;
        this.quantidade_funcionarios = quantidade_funcionarios;
        this.filial_estado = filial_estado.ToUpper();
        this.lista_funcionarios = new List<Funcionario>();

    }

    //Construtor sem parametro 
    public Filial()
    {
        cnpj = "0";
        nome = "sem nome";
        quantidade_funcionarios = 0;
        filial_estado = " ";

    }

}
