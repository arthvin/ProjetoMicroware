using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto01
{
    internal class Controller
    {
        public class Microwave
        {
            private const int MaxTimeInSeconds = 120;  // 2 minutos
            private const int MinTimeInSeconds = 1;    // 1 segundos
            private const int DefaultQuickStartTime = 30; // Tempo de início rápido
            private int _timeRemaining;
            private int _power;
            private bool _isRunning;
            private bool _isPaused;
            private List<HeatingProgram> _predefinedPrograms;

            public string HeatingString { get; private set; }

            public Microwave()
            {
                _predefinedPrograms = new List<HeatingProgram>
                {
                new HeatingProgram("Pipoca", "Pipoca (de micro-ondas)", 180, 7, "PIPOCANDO", "Observar o barulho de estouros do milho..."),
                new HeatingProgram("Leite", "Leite", 300, 5, "ESQUENTANDO", "Cuidado com aquecimento de líquidos..."),
                new HeatingProgram("Carnes de boi", "Carne em pedaço ou fatias", 840, 4, "DESCONGELANDO", "Interrompa o processo na metade..."),
                new HeatingProgram("Frango", "Frango (qualquer corte)", 480, 7, "DESCONGELANDO", "Interrompa o processo na metade..."),
                new HeatingProgram("Feijão", "Feijão congelado", 480, 9, "DESAQUECENDO", "Deixe o recipiente destampado...")
                };
            }

            public void StartHeating(int timeInSeconds, int? powerLevel = null, Label edtLabel = null)
            {
                if (_isRunning)
                {
                    // Adiciona tempo se o micro-ondas já estiver em execução
                    AddTime(edtLabel);
                }
                else
                {
                    if (timeInSeconds < MinTimeInSeconds || timeInSeconds > MaxTimeInSeconds)
                    {
                        edtLabel.Text = ($"Erro: O tempo deve ser entre {MinTimeInSeconds} segundo e {MaxTimeInSeconds / 60} minutos.");
                        Application.DoEvents();
                        return;
                    }

                    if (powerLevel < 1 || powerLevel > 10)
                    {
                        edtLabel.Text = ("Erro: A potência deve ser entre 1 e 10.");
                        Application.DoEvents();
                        return;
                    }

                    // Definir tempo restante e potência
                    _timeRemaining = timeInSeconds;

                    // Formatar e exibir o tempo
                    string formattedTime = FormatTime(_timeRemaining);
                    edtLabel.Text = ($"Aquecendo por {formattedTime} com potência {powerLevel}.");
                    Application.DoEvents();

                    // Iniciar aquecimento
                    _isRunning = true;
                    _isPaused = false;
                    HeatingString = GenerateHeatingString(_timeRemaining, _power);
                    RunMicrowave(edtLabel);
                }
            }

            public void StartQuick(int power, Label edtLabel = null)
            {
                StartHeating(DefaultQuickStartTime, power, edtLabel);
            }

            public void AddTime(Label edtLabel)
            {
                if (_isRunning && !_isPaused)
                {
                    _timeRemaining += 30;
                    edtLabel.Text = ("30 segundos adicionados.");
                    HeatingString = GenerateHeatingString(_timeRemaining, _power);
                }
                else
                {
                    edtLabel.Text = ("Erro: O aquecimento não está em andamento.");
                }
            }

            public void PauseOrCancel(Label edtLabel)
            {
                if (_isRunning)
                {
                    if (_isPaused)
                    {
                        // Cancelar
                        _isRunning = false;
                        _timeRemaining = 0;
                        HeatingString = string.Empty;
                        edtLabel.Text = ("Aquecimento cancelado.");
                    }
                    else
                    {
                        // Pausar
                        _isPaused = true;
                        edtLabel.Text = ("Aquecimento pausado.");
                    }
                }
                else
                {
                    // Limpar dados
                    _timeRemaining = 0;
                    HeatingString = string.Empty;
                    edtLabel.Text = ("Informações limpas.");
                }
            }

            private void RunMicrowave(Label edtLabel = null)
            {
                while (_timeRemaining > 0 && _isRunning && !_isPaused)
                {
                    Thread.Sleep(1000);  // Simular 1 segundo de aquecimento
                    _timeRemaining--;
                    edtLabel.Text = (HeatingString.Substring(0, (_timeRemaining * _power) / MaxTimeInSeconds) + " aquecendo...");
                    Application.DoEvents();
                }

                if (_isRunning && !_isPaused)
                {
                    edtLabel.Text = (HeatingString + " Aquecimento concluído.");
                    Application.DoEvents();
                }
            }

            private string FormatTime(int timeInSeconds)
            {
                if (timeInSeconds > 60 && timeInSeconds < 100)
                {
                    int minutes = timeInSeconds / 60;
                    int seconds = timeInSeconds % 60;
                    return $"{minutes}:{seconds:D2} minutos";
                }
                return $"{timeInSeconds} segundos";
            }

            private string GenerateHeatingString(int time, int power)
            {
                int totalDots = time * power;
                return new string('.', totalDots);
            }

            public class HeatingProgram
            {
                public string Name { get; }
                public string Food { get; }
                public int TimeInSeconds { get; }
                public int PowerLevel { get; }
                public string HeatingString { get; }
                public string Instructions { get; }

                public HeatingProgram(string name, string food, int timeInSeconds, int powerLevel, string heatingString, string instructions)
                {
                    Name = name;
                    Food = food;
                    TimeInSeconds = timeInSeconds;
                    PowerLevel = powerLevel;
                    HeatingString = heatingString;
                    Instructions = instructions;
                }
            }
            public void StartPredefinedProgram(string programName, Label edtLabel)
            {
                HeatingProgram program = _predefinedPrograms.FirstOrDefault(p => p.Name == programName);
                if (program != null)
                {
                    // Definir tempo restante e potência do programa selecionado
                    _timeRemaining = program.TimeInSeconds;
                    _power = program.PowerLevel;

                    // Formatar e exibir o tempo
                    string formattedTime = FormatTime(_timeRemaining);
                    edtLabel.Text = $"Aquecendo {program.Food} por {formattedTime} com potência {_power}.";
                    Application.DoEvents();

                    // Iniciar aquecimento com as regras específicas do programa
                    _isRunning = true;
                    _isPaused = false;
                    HeatingString = program.HeatingString;
                    RunMicrowave(edtLabel);
                }
                else
                {
                    edtLabel.Text = "Erro: Programa não encontrado.";
                    Application.DoEvents();
                }
            }


        }
    }
}
