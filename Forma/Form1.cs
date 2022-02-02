using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forma
{
    
    
    
    class Device
    {
        public string Name { get; set; }
    }

    class Input
    {
        public string Name { get; set; } = "Input";
        public bool IsOn { get; set; } = false;
        public Device AttachedTo { get; set; }
    }
    
    public partial class Form1 : Form
    {
        List<List<Input>> list = new List<List<Input>>();

        Dictionary<string, Input> inputs = new Dictionary<string, Input>();
        public Form1()
        {
            InitializeComponent();
            Init();
        }
        
        
        private void Init()
        {

            for (int i = 0; i < 14; i++)
            {
                var inputSignals = new Input { Name = $"I{i}", IsOn = false, AttachedTo = new Device { Name = "None" } };
                inputs.Add(inputSignals.Name, inputSignals);
                checkedListBox1.Items.Add(inputSignals.Name, inputSignals.IsOn);
            }
            pictureBox1.Image = Properties.Resources.lamp_off;

            Dictionary<string, Input> outputs = new Dictionary<string, Input>();

            for (int i = 0; i < 10; i++)
            {
                var outputSignals = new Input { Name = $"Q{i}", IsOn = false, AttachedTo = new Device { Name = "None" } };
                outputs.Add(outputSignals.Name, outputSignals);
            }
        }

        private void CheckLadderScript(List<Input> ladderLogic)
        {

            for (int i = 0; i < ladderLogic.Count; i++)
            {
                inputs[ladderLogic[i].Name] = ladderLogic[i];
                checkedListBox1.Items.Clear();
                foreach (var it in inputs)
                {
                    checkedListBox1.Items.Add(it.Key, it.Value.IsOn);
                }
            }

            string temp = string.Empty;
            string outputName = ladderLogic.Last().Name;
            var device = inputs[outputName];

            if (ladderLogic.FindAll(x => x.Name.StartsWith("I")).TrueForAll(it => it.IsOn))
            {
                device.IsOn = !device.IsOn;
                Console.WriteLine($"{device.AttachedTo.Name } Switched on! ");
                pictureBox1.Image = Properties.Resources.lamp_on;
                checkedListBox1.SetItemChecked(checkedListBox1.Items.Count - 1, device.IsOn);
            }
            else
            {
                Console.WriteLine($"Not enough power! Switching off {device.Name}!");
                device.IsOn = false;
                pictureBox1.Image = Properties.Resources.lamp_off;
                checkedListBox1.SetItemChecked(checkedListBox1.Items.Count - 1, device.IsOn);
            }

            foreach (var it in inputs)
            {
                Console.WriteLine($"name: { it.Key }, State: { it.Value.IsOn }");
            }

        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
           // throw new System.NotImplementedException();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var I0 = new Input { Name = $"I0", IsOn = true, AttachedTo = new Device { Name = "Button1" } };
            var I1 = new Input { Name = $"I1", IsOn = true, AttachedTo = new Device { Name = "Button2" } };
            var I2 = new Input { Name = $"I2", IsOn = true, AttachedTo = new Device { Name = "Button3" } };
            var Q0 = new Input { Name = $"Q0", IsOn = false, AttachedTo = new Device { Name = "Lamp" } };


            var ladderLogic = new List<Input> { I0, I1, I2, Q0 };

            CheckLadderScript(ladderLogic);

            List<Input> second = new List<Input> { I0, I1, I2, Q0 };
            second[0].IsOn = false;
            //CheckLadderScript(second);
        }
     
            
     
        Input I0 = new Input { Name = $"I0", IsOn = true, AttachedTo = new Device { Name = "Button1" } };
        Input I1 = new Input { Name = $"I1", IsOn = true, AttachedTo = new Device { Name = "Button2" } };
        Input I2 = new Input { Name = $"I2", IsOn = true, AttachedTo = new Device { Name = "Button3" } };
        Input Q0 = new Input { Name = $"Q0", IsOn = false, AttachedTo = new Device { Name = "Lamp" } };
        private void button2_Click(object sender, EventArgs e)
        {
            
            foreach (var item in checkedListBox1.Items)
            {
                //MessageBox.Show(item.ToString());
                inputs[item.ToString()].IsOn = false;
            }
            
            foreach (var item in checkedListBox1.CheckedItems)
            {
                //MessageBox.Show(item.ToString());
                inputs[item.ToString()].IsOn = true;
            }
       
            
            var ladderLogic = new List<Input> { inputs["I0"], inputs["I1"], inputs["I2"], inputs["Q0"]};

            CheckLadderScript(ladderLogic);
            
            
            //CheckLadderScript(ladderLogic);
            foreach (var it in inputs)
            {
                MessageBox.Show($"{it.Key} {it.Value.IsOn}");    
            }
          
        }
    }
}