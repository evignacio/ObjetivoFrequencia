﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Forms;
using System.IO; //declarando a biblioteca de entrada e saída de arquivos
                 //a biblioteca IO



namespace Pj_FrquenciaObjetivo
{
    public partial class registros : MetroFramework.Forms.MetroForm
    {
        public registros()
        {
            InitializeComponent();
        }

        private void registros_Load(object sender, EventArgs e)
        {
            if (Controller.L_alunos1.Count()!=0)
            {
                for (int i = Controller.L_alunos1.Count-1; i >= 0; i--)
                {

                    Controller.L_alunos1.RemoveAt(i);

                }
            }
            for (int i = 0; i < gridAlunosNovos.RowCount; i++)
            {
               gridAlunosNovos.Rows[i].DataGridView.Columns.Clear();
            }
            gridAlunosNovos.DataSource = null;
            gridAlunosNovos.Refresh();
            gridAlunosNovos.Columns.Add("Matricula", "Matricula");
            gridAlunosNovos.Columns.Add("Nome", "Nome");
            gridAlunosNovos.Columns.Add("Status", "Status");

            
            Controller.GetAlunos();
            Controller.L_alunos1.Count();
            foreach (Aluno al in Controller.L_alunos1)
            {
                try
                {

                    if (al.Nome1=="" && al.Status1=="ATIVO")
                    {

                       gridAlunosNovos.Rows.Add(al.Matricula1,al.Nome1,al.Status1
                           
                           );

                    }
                }

                catch (Exception)
                {
                    MetroMessageBox.Show(this, "Falha ao tentar realizar o filtro, por favor tente novamente.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
          
            
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {

        }

        private void btn_voltar_Click(object sender, EventArgs e)
        {
            Home Hhome = new Home();
            this.Hide(); // use dessa maneira.
            Hhome.ShowDialog();
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            
            int qtd = Controller.L_alunos1.Count() - 1, cont = 0;
            try
          {
                // Salvando dados alterados no grid
                foreach (Aluno al in Controller.L_alunos1)
                {
                
                   

                    if (cont<=qtd)
                     {

                        if (al.Matricula1.Equals(gridAlunosNovos.Rows[cont].Cells["Matricula"].Value.ToString()))
                        {
                            al.Nome1 = gridAlunosNovos.Rows[cont].Cells["Nome"].Value.ToString();
                            cont++;
                        }
                        
                        
                     }
                   
                }
                MessageBox.Show("Nome alterados com sucesso ");
                Controller.AlteraNomes();

            }

         catch (Exception er)
          {
                MessageBox.Show("erro"+er);
          }
           gridAlunosNovos.Refresh();







        }

        private void gridAlunosNovos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Controller.ExcAluno1 = gridAlunosNovos.Rows[e.RowIndex].Cells["Matricula"].Value.ToString();
           
         
         



        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            try
            {
                Controller.ExcluiAluno(Controller.ExcAluno1);
                MessageBox.Show("Aluno Excluid com sucesso !");

                for (int i = 0; i < gridAlunosNovos.RowCount; i++)
                {
                    gridAlunosNovos.Rows[i].DataGridView.Columns.Clear();
                }
                gridAlunosNovos.DataSource = null;
                gridAlunosNovos.Refresh();
                gridAlunosNovos.Columns.Add("Matricula", "Matricula");
                gridAlunosNovos.Columns.Add("Nome", "Nome");
                gridAlunosNovos.Columns.Add("Status", "Status");
                Controller.GetAlunos();

                foreach (Aluno al in Controller.L_alunos1)
                {
                    try
                    {

                        if (al.Nome1 == "" && al.Status1 == "ATIVO")
                        {

                            gridAlunosNovos.Rows.Add(al.Matricula1, al.Nome1, al.Status1

                                );

                        }
                    }

                    catch (Exception)
                    {
                        MetroMessageBox.Show(this, "Falha ao tentar realizar o filtro, por favor tente novamente.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }
            catch (Exception error)
            {
                MessageBox.Show("Erro ao tentar excluir o aluno: "+error);

            }
        }
    }
}
