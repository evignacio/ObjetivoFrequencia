﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace Pj_FrquenciaObjetivo
{
    class Controller
    {
        private static List<Apontamento> L_apontamento = new List<Apontamento>();
        private static List<Apontamento> L_execoes = new List<Apontamento>();
        private static Thread Thread;

        internal static List<Apontamento> L_apontamento1 { get => L_apontamento; set => L_apontamento = value; }
        internal static List<Apontamento> L_execoes1 { get => L_execoes; set => L_execoes = value; }
        public static Thread Thread1 { get => Thread; set => Thread = value; }


        public static void Splash()
        {
            SplashScreen.SplashForm frm = new SplashScreen.SplashForm();
            frm.AppName = "Por favor aguarde";
            Application.Run(frm);

        }

        public static void CriaLoad()
        {
            Thread = new Thread(new ThreadStart(Splash));
        }

        public static void CarregaApontamentos(Apontamento Aponta)
        {

            L_apontamento1.Add(Aponta);


        }

        public static int TrataEx()
        {

            string[] remove = new string[L_apontamento1.Count()];
            bool achou = false;
            int contador = 0, rmoveCont=0;
            int total = Controller.L_apontamento1.Count() - 1;

            foreach (Apontamento ap in L_apontamento1)
            {


                foreach (Apontamento ap2 in L_apontamento1)
                {
                    achou = false;



                    if (ap.Mes.Equals(ap2.Mes) && ap.Dia.Equals(ap2.Dia) && ap.Matricula_aluno.Equals(ap2.Matricula_aluno) && ap.Status == "Entrada" && ap2.Status == "Saida")
                    {



                        achou = true;
                        contador = 0;
                        break;



                    }

                    if (ap.Mes.Equals(ap2.Mes) && ap.Dia.Equals(ap2.Dia) && ap.Matricula_aluno.Equals(ap2.Matricula_aluno) && ap.Status == "Saida" && ap2.Status == "Entrada")
                    {



                        achou = true;
                        contador = 0;
                        break;



                    }
                    else if (achou == false && total == contador)
                    {
                        ap.Tipo = "Exceção";                      
                        L_execoes.Add(ap);                  
                        L_execoes.Count();
                        achou = false;
                        contador = 0;
                        remove[rmoveCont] = ap.Matricula_aluno;
                        rmoveCont++;
                        break;
                    }
                    contador++;


                    


                    }             
                }
            // removendo as exceções dos apotamentos

            for (int i = 0; i <= rmoveCont; i++)
            {

                L_apontamento1.Remove(L_apontamento1[i]);


            }
            L_apontamento1.Count();
            return L_execoes.Count();
        }





        public static int GetQtEntrada()
        {
            int entrada = 0;
            foreach (Apontamento ap in L_apontamento1)
            {


                foreach (Apontamento ap2 in L_apontamento1)
                {
                    if (ap.Status == "Entrada")
                    {

                        entrada++;
                        break;

                    }
                }


            }
            return entrada;
        }



        public static int GetqtSaida()
        {
            int saida = 0;
            foreach (Apontamento ap in L_apontamento1)
            {

                foreach (Apontamento ap2 in L_apontamento1)
                {
                    if (ap.Status == "Saida")
                    {

                        saida++;
                        break;

                    }
                }

            }
            return saida;



        }

        public static int SetDadosBanco()
        {
            int  qtNovos=0, linhas=0;
            string connString = "Server=USUARIO-PC\\SQLEXPRESS; Database=objetivo; Integrated Security=True;";
            SqlConnection conexao = new SqlConnection(connString); /* conexao irá conectar o C# ao banco de dados */
            conexao.Open(); // abre a conexão com o banco  
            try
            {
                // adicionando alunos
                foreach (Apontamento ap in L_apontamento1)
                {
                    SqlCommand select = new SqlCommand("SELECT Matricula From Alunos Where Matricula='"+ap.Matricula_aluno+"'",conexao);



                    SqlDataReader resultado = select.ExecuteReader();
                   

                    while (resultado.Read())
                    {
                        linhas++;
                    }
                    resultado.Close();

                    if (linhas==0)
                    {
                        qtNovos++;
                        SqlCommand cmd = new SqlCommand("INSERT INTO Alunos values ( '" + ap.Matricula_aluno + "','')", conexao); /*cmd possui mais de um parâmetro, neste caso coloquei o comando SQL "SELECT * FROM tabela" que irá selecionar tudo(*) de tabela, o segundo parâmetro indica onde o banco está conectado,ou seja se estamos selecionando informações do banco precisamos dizer onde ele está localizado */
                        cmd.ExecuteNonQuery(); // executa cmd
                    }
                    linhas = 0;



                   
                }
                // Adicionando apontamentos 
                foreach(Apontamento aponta in L_apontamento1)
                {

                    SqlCommand verificaAp = "";

                }



                return qtNovos;
            }

            catch (Exception e)
            {
                MessageBox.Show("erro"+e);
                return qtNovos;
            }
             finally
            {
                conexao.Close();
            }


        }                  /*Pronto após o cmd.ExecuteNonQuery(); selecionamos tudo o que tinha dentro do banco, agora os passos seguintes irão exibir as informações para que o usuário possa vê-las    */

    







    }


}
