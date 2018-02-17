using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Speech.Synthesis;

namespace Speech_recognize
{
    public partial class Form1 : Form
    {
        SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();
       

        public Form1()
        {
            InitializeComponent();
        }

        private void btnEnable_Click(object sender, EventArgs e)
        {
            recEngine.RecognizeAsync(RecognizeMode.Multiple);
            btnDisable.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Choices commands = new Choices();
            commands.Add(new string[] { "hello", "print my name", "hello kalana", "open new form" });
            GrammarBuilder gBuilder = new GrammarBuilder();
            gBuilder.Append(commands);
            Grammar grammer = new Grammar(gBuilder);

            recEngine.LoadGrammarAsync(grammer);
            recEngine.SetInputToDefaultAudioDevice();
            recEngine.SpeechRecognized += recEngine_SpeechRecognized;
           
        }

        void recEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch (e.Result.Text) 
            {
                case "hello kalana":
                    MessageBox.Show("Hello Kalana. How are You?");
                    break;
                case "print my name":
                    textBox1.Text += "\nKalana\n";
                    break;
                case "open new form":
                    Form2 nwfm2 = new Form2();
                    nwfm2.Show();
                    this.Show();
                    break;
                case "hello":
                    SpeechSynthesizer synthesizer = new SpeechSynthesizer();
                    synthesizer.Speak("hello user. how are u?");
                    synthesizer.Dispose();
                    break;
            }

            //if (e.Result.Text == "hello computer")
            //{
            //    Form2 nwfm2 = new Form2();
            //    nwfm2.Show();
            //    this.Show();
            //}
        }

        private void btnDisable_Click(object sender, EventArgs e)
        {
            recEngine.RecognizeAsyncStop();
            btnDisable.Enabled = false;
        }
    }
}
