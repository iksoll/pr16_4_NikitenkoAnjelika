using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private List<Country> countries = new List<Country>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadCountriesFromTextFile("countries.txt");

            var filteredCountries = countries
                .Where(c => c.Population > 104000000)
                .OrderBy(c => c.Name.Length)
                .ThenBy(c => c.Name)
                .ToList();

            foreach (var country in filteredCountries)
            {
                listBox1.Items.Add(country.Name + " - " + country.Population.ToString("#,##0"));
            }
        }

        private void LoadCountriesFromTextFile(string fileName)
        {
            try
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] parts = line.Split(',');
                        string name = parts[0];
                        int population = int.Parse(parts[1]);
                        countries.Add(new Country(name, population));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading file: " + ex.Message);
            }
        }
    }

    public class Country
    {
        public string Name { get; set; }
        public int Population { get; set; }

        public Country(string name, int population)
        {
            Name = name;
            Population = population;
        }
    }
}
