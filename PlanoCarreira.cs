using System;
using System.Collections.Generic;


public class PlanoCarreira
{
    public float tempo_de_empresa { get; protected set; }
    private bool avaliacao_gerente = true;
    private Funcionario funcionario;



    public PlanoCarreira(Funcionario funcionario)
    {
        this.tempo_de_empresa = funcionario.CalcularDiasNaEmpresa() / 30;
        this.funcionario = funcionario;
    }



  
    public void ExibirFuncionarios(List<Funcionario> lista)
    {
        Console.WriteLine("==================================================");
        Console.WriteLine("-------------FUNCIONÁRIOS CADASTRADOS-------------");
        Console.WriteLine("==================================================\n\n");

        for (int i = 0; i < lista.Count; i++)
        {
            Funcionario f = lista[i];
            Console.WriteLine($"Funcionário {i + 1}: {f.nome}");
        }

    }

// Método relatório do funcionario após escolhido para visualizar o plano de carreira;
    public void InfoDoFuncionario(Funcionario f, PlanoCarreira p)
    {
        Console.WriteLine($"\nNome: {f.nome}\nCargo: {f.cargo}\nSalario: {f.salario}\nTempo de empresa em DIAS: {f.CalcularDiasNaEmpresa()}\nTempo de empresa em MESES: {p.tempo_de_empresa}\n");
    }

  
    // Método pra verificar se está apto a subir de cargo:
    public void SubirCargo()
    {
        Console.WriteLine("\nPassou na avaliação do gerente? [S]/[N]:  ");
        avaliacao_gerente = (char.ToUpper(Console.ReadLine()![0]) == 'N') ? false : avaliacao_gerente;

        if (tempo_de_empresa >= 8 && avaliacao_gerente)
        {
            Console.WriteLine("==================================================");
            Console.WriteLine("\n[✓]  Avaliação do Gerente\n[✓]  Tempo de empresa maior que 8 meses");
            Console.WriteLine("\n>> APROVADO, apto a subir de cargo.\n");
            Console.WriteLine("==================================================");

        }

        else if (tempo_de_empresa < 8 && avaliacao_gerente)
        {
            Console.WriteLine("==================================================");
            Console.WriteLine("\n[✓]  Avaliação do Gerente\n[x]  Tempo de empresa maior que 8 meses");
            Console.WriteLine("\n>> REPROVADO, aprovado pela vistoria do gerente, porém sem tempo suficiente de empresa.\n");
            Console.WriteLine("==================================================");

        }
        else if (tempo_de_empresa > 8 && avaliacao_gerente == false)
        {
            Console.WriteLine("==================================================");
            Console.WriteLine("\n[x]  Avaliação do Gerente\n[✓]  Tempo de empresa maior que 8 meses");
            Console.WriteLine("\n>> REPROVADO, possui tempo de empresa porém não passou pela vistoria do gerente.\n");
            Console.WriteLine("==================================================");
        }
        else
        {
            Console.WriteLine("=====================================================");
            Console.WriteLine("\n[x]  Avaliação do Gerente\n[x]  Tempo de empresa maior que 8 meses");
            Console.WriteLine("\n>> REPROVADO, não está apto a subir de cargo.\n");
            Console.WriteLine("=====================================================");
        }
    }

  
}