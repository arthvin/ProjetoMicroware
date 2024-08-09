using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Projeto01.Controller;

namespace Projeto01
{
    public partial class Main : Form
    {
        private int _power = 10;
        private Microwave microwave;
        private List<Program> _customPrograms;

        public class Program
        {
            public string Nome { get; set; }
            public string Alimento { get; set; }
            public int Potencia { get; set; }
            public int Tempo { get; set; }
            public string Caractere { get; set; }
            public string Instrucoes { get; set; }
        }

        public Main()
        {
            microwave = new Microwave();

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            microwave.StartQuick(_power, lblMicroondas);
        }
        private void btnIniciar_Click(object sender, EventArgs e)
        {
            if (Int32.Parse(edtMinutos.Text) > 0) {
                microwave.StartHeating(Int32.Parse(edtMinutos.Text), _power, lblMicroondas);
            }
            else
            {
                microwave.StartHeating(10, _power, lblMicroondas);
            }

            edtMinutos.Text = "";
        }
        private void btnPotencia_Click(object sender, EventArgs e)
        {
            if (Int32.Parse(edtMinutos.Text) > 0 || Int32.Parse(edtMinutos.Text) <= 10)
            {
                _power = Int32.Parse(edtMinutos.Text);
            }
            else
            {
                lblMicroondas.Text = "Potencia só pode ser definida de 1 até 10";
            }

            edtMinutos.Text = "";
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            edtMinutos.Text = (edtMinutos.Text + '1');
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            edtMinutos.Text = (edtMinutos.Text + '2');
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            edtMinutos.Text = (edtMinutos.Text + '3');
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            edtMinutos.Text = (edtMinutos.Text + '4');
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            edtMinutos.Text = (edtMinutos.Text + '5');
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            edtMinutos.Text = (edtMinutos.Text + '6');
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            edtMinutos.Text = (edtMinutos.Text + '7');
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            edtMinutos.Text = (edtMinutos.Text + '8');
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            edtMinutos.Text = (edtMinutos.Text + '9');
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            edtMinutos.Text = (edtMinutos.Text + '0');
        }

        private void btnPausaCancela_Click(object sender, EventArgs e)
        {
            microwave.PauseOrCancel(lblMicroondas);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            microwave.StartPredefinedProgram("Pipoca", lblMicroondas);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            microwave.StartPredefinedProgram("Leite", lblMicroondas);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            microwave.StartPredefinedProgram("Frango", lblMicroondas);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            microwave.StartPredefinedProgram("Feijão", lblMicroondas);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            microwave.StartPredefinedProgram("Carnes de Boi", lblMicroondas);
        }
        private void btnAdicionarPrograma_Click(object sender, EventArgs e)
        {
            string nome = txtNomePrograma.Text;
            string alimento = txtAlimentoPrograma.Text;
            int potencia;
            int tempo;
            string caractere = txtCaracterePrograma.Text;

            if (string.IsNullOrWhiteSpace(nome) ||
                string.IsNullOrWhiteSpace(alimento) ||
                !int.TryParse(txtPotenciaPrograma.Text, out potencia) ||
                !int.TryParse(txtTempoPrograma.Text, out tempo) ||
                string.IsNullOrWhiteSpace(caractere) ||
                caractere == ".")
            {
             
                return;
            }

            Program novoPrograma = new Program
            {
                Nome = nome,
                Alimento = alimento,
                Potencia = potencia,
                Tempo = tempo,
                Caractere = caractere,
            };

            _customPrograms.Add(novoPrograma);
        }

        private void txtCaracterePrograma_DragLeave(object sender, EventArgs e)
        {
            if (txtCaracterePrograma.TextLength > 1)
            {
                txtCaracterePrograma.Clear();
            }
        }
    }
}
