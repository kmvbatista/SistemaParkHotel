﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFPresentationLayer
{
    public partial class FormSelecionarCheckin : Form
    {
        public int QuemChamou { get; set; }

        public FormSelecionarCheckin(List<DAO.ViewModels.CheckinViewModel> list)
        {
            dataGridDialog1.DataSource=list;
            InitializeComponent();
        }
    }
}
