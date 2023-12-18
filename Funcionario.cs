using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;


///Classe Base
public class Funcionario
{

    public string nome { get; protected set; }
    public string cargo { get; protected set; }
    public float salario { get; protected set; }
    public DateTime data_entrada { get; protected set; }
    public int codigo_filial { get; protected set; }


    ///Construtor sem parametro
    public Funcionario()
    {
        nome = "vazio";
        cargo = "vazio";
        salario = 0f;

    }

    ///Construtor com parametro
    public Funcionario(string nome, string cargo, float salario, DateTime data_entrada, int codigo_filial)
    {
        this.nome = nome.ToUpper();
        this.cargo = cargo.ToUpper();
        this.salario = salario;
        this.data_entrada = data_entrada;
        this.codigo_filial = codigo_filial;

    }


    /////
    // Metodo pra verificar quanto tempo está na empresa:
    public int CalcularDiasNaEmpresa()
    {
        DateTime dataAtual = DateTime.Now;
        TimeSpan diferenca = dataAtual - data_entrada;
        return diferenca.Days;
    }

    public void ExibirTempoNaEmpresaEmAnos()
    {
        DateTime dataAtual = DateTime.Now;
        TimeSpan diferenca = dataAtual - data_entrada;
        int anos = (int)(diferenca.Days / 365.25);
        Console.WriteLine($"\n{nome} está na empresa há aproximadamente {anos} anos.");
    }

    public void ExibirDadosAposCadastro()
    {
        Console.WriteLine($"\nNome: {nome}");
        Console.WriteLine($"Cargo: {cargo}");
        Console.WriteLine($"Salário: {salario}");
        Console.WriteLine($"Data de Entrada: {data_entrada.ToShortDateString()}");
        Console.WriteLine($"Código da Filial: {codigo_filial}");

    }
}