using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace AsignacionDeMemoria
{
    public partial class Form1 : Form
    {
        // ====== Datos ======
        public class MemoryBlock
        {
            public int Id { get; set; } 
            public int StartAddress { get; set; } 
            public int Size { get; set; } // Tamaño total del bloque
            public int UsedSize { get; set; }      // KB que realmente usa el proceso
            public bool IsFree { get; set; }       //Libre u ocupado
            public string ProcessName { get; set; } 
            public MemoryBlock Clone() { return (MemoryBlock)MemberwiseClone(); }
        }

        public class ProcessRecord
        {
            public string Name { get; set; }
            public int Size { get; set; }
            public string Status { get; set; }
            public ProcessRecord Clone() { return (ProcessRecord)MemberwiseClone(); }
        }

        private List<MemoryBlock> memoryBlocks = new List<MemoryBlock>(); //Memoria completa
        private List<ProcessRecord> processList = new List<ProcessRecord>(); //Historial de procesos
        private int blockIdCounter = 1;
        private List<RectangleF> rectBloques = new List<RectangleF>();   // para poder hacer clic en la grafica

        // ====== Constructor ======
        public Form1()
        {
            InitializeComponent();
            ConfigurarDataGridView();
            ConfigurarComboBoxEstrategia();
            ConfigurarGrafica();

            // Se conectan los eventos aqui. El "-=" evita que se dupliquen si el Designer ya los conecto.
            btnInitMemory.Click -= btnInitMemory_Click; btnInitMemory.Click += btnInitMemory_Click;
            btnAllocate.Click -= btnAllocate_Click; btnAllocate.Click += btnAllocate_Click;
            btnDeallocate.Click -= btnDeallocate_Click; btnDeallocate.Click += btnDeallocate_Click;
            btnReset.Click -= btnReset_Click; btnReset.Click += btnReset_Click;
            btnModificar.Click -= btnModificar_Click; btnModificar.Click += btnModificar_Click;
            cmbEstrategia.SelectedIndexChanged -= cmbEstrategia_SelectedIndexChanged; cmbEstrategia.SelectedIndexChanged += cmbEstrategia_SelectedIndexChanged;
        }

        // ====== Configuracion de tablas y combo ======
        private void ConfigurarGrid(DataGridView dgv)
        {
            dgv.AutoGenerateColumns = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.ReadOnly = true;
            dgv.MultiSelect = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.RowHeadersVisible = false;
            dgv.Columns.Clear();
        }

        private void ConfigurarDataGridView()
        {
            // Tabla de memoria
            ConfigurarGrid(dgvMemory);
            dgvMemory.Columns.Add(new DataGridViewTextBoxColumn { Name = "Id", HeaderText = "ID", DataPropertyName = "Id", Width = 40 });
            dgvMemory.Columns.Add(new DataGridViewTextBoxColumn { Name = "StartAddress", HeaderText = "Dir. Inicio", DataPropertyName = "StartAddress", Width = 75 });
            dgvMemory.Columns.Add(new DataGridViewTextBoxColumn { Name = "Size", HeaderText = "Tamaño (KB)", DataPropertyName = "Size", Width = 90 });
            dgvMemory.Columns.Add(new DataGridViewTextBoxColumn { Name = "UsedSize", HeaderText = "Usado (KB)", DataPropertyName = "UsadoTexto", Width = 85 });
            dgvMemory.Columns.Add(new DataGridViewTextBoxColumn { Name = "Frag", HeaderText = "Frag. interna", DataPropertyName = "FragTexto", Width = 85 });
            dgvMemory.Columns.Add(new DataGridViewTextBoxColumn { Name = "IsFree", HeaderText = "Estado", DataPropertyName = "EstadoTexto", Width = 70 });
            dgvMemory.Columns.Add(new DataGridViewTextBoxColumn { Name = "ProcessName", HeaderText = "Proceso", DataPropertyName = "ProcessNameText", Width = 80 });
            foreach (DataGridViewColumn c in dgvMemory.Columns)
                c.SortMode = DataGridViewColumnSortMode.NotSortable;   // si se ordena, el indice de la fila ya no coincide con la lista

            // Tabla de procesos
            ConfigurarGrid(dgvProcesos);
            dgvProcesos.Columns.Add(new DataGridViewTextBoxColumn { Name = "ProcName", HeaderText = "Proceso", DataPropertyName = "Name", Width = 65 });
            dgvProcesos.Columns.Add(new DataGridViewTextBoxColumn { Name = "ProcSize", HeaderText = "KB", DataPropertyName = "Size", Width = 55 });
            dgvProcesos.Columns.Add(new DataGridViewTextBoxColumn { Name = "ProcStatus", HeaderText = "Estado", DataPropertyName = "Status", Width = 75 });
            foreach (DataGridViewColumn c in dgvProcesos.Columns)
                c.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        //Configuracion de la seleccion de ajuste
        private void ConfigurarComboBoxEstrategia()
        {
            cmbEstrategia.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstrategia.Items.Clear();
            cmbEstrategia.Items.Add("Primer Ajuste (First Fit)");
            cmbEstrategia.Items.Add("Mejor Ajuste (Best Fit)");
            cmbEstrategia.Items.Add("Peor Ajuste (Worst Fit)");
            cmbEstrategia.SelectedIndex = 1;
        }

        // ====== Actualizar pantalla ======
        private void ActualizarVista()
        {
            var memoryDataSource = memoryBlocks.Select(b => new
            {
                b.Id,
                b.StartAddress,
                b.Size,
                UsadoTexto = b.IsFree ? "-" : b.UsedSize.ToString(),
                FragTexto = b.IsFree ? "-" : (b.Size - b.UsedSize).ToString(),
                EstadoTexto = b.IsFree ? "Libre" : "Ocupado",
                ProcessNameText = b.IsFree ? "-" : b.ProcessName
            }).ToList();

            dgvMemory.DataSource = null;
            dgvMemory.DataSource = memoryDataSource;

            foreach (DataGridViewRow row in dgvMemory.Rows)
            {
                string estado = row.Cells["IsFree"].Value?.ToString();
                row.DefaultCellStyle.BackColor = (estado == "Libre") ? Color.LightGreen : Color.LightCoral;
            }
            dgvMemory.ClearSelection();

            dgvProcesos.DataSource = null;
            dgvProcesos.DataSource = processList.ToList();
            dgvProcesos.ClearSelection();

            pnlGrafica.Invalidate();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void dgvProcesses_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        // ====== Cambiar de algoritmo: se vuelven a asignar los procesos con el nuevo ajuste ======
        private void cmbEstrategia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (memoryBlocks.Count == 0) return;   // aun no se inicia la memoria
            ReasignarTodos();
        }

        private void ReasignarTodos()
        {
            // Se liberan todos los bloques
            foreach (MemoryBlock b in memoryBlocks)
            {
                b.IsFree = true;
                b.ProcessName = null;
                b.UsedSize = 0;
            }

            // Se vuelven a asignar los procesos activos, en el mismo orden en que llegaron
            var original = processList.ToList();
            processList = new List<ProcessRecord>();
            var sinEspacio = new List<string>();

            foreach (ProcessRecord rec in original)
            {
                if (rec.Status == "Finalizado")
                {
                    processList.Add(rec);
                    continue;
                }

                string error;
                if (!AsignarProceso(rec.Name, rec.Size, out error))
                {
                    processList.Add(new ProcessRecord { Name = rec.Name, Size = rec.Size, Status = "Sin espacio" });
                    sinEspacio.Add(rec.Name);
                }
            }

            ActualizarVista();

            if (sinEspacio.Count > 0)
            {
                MessageBox.Show("Con " + cmbEstrategia.SelectedItem + " no cupieron: " + string.Join(", ", sinEspacio) + ".",
                    "Sin espacio", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // ====== Iniciar memoria con N bloques ======
        private bool LeerBloques(int total, out List<int> lista)
        {
            lista = new List<int>();
            string txt = txtBloques.Text.Trim();

            // Si se deja vacio: un solo bloque con toda la memoria
            if (txt == "")
            {
                lista.Add(total);
                return true;
            }

            string[] partes = txt.Split(new[] { ',', ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < partes.Length; i++)
            {
                int v;
                if (!int.TryParse(partes[i], out v) || v <= 0)
                {
                    MessageBox.Show("El bloque " + (i + 1) + " ('" + partes[i] + "') no es un numero valido mayor a 0.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (v > total)
                {
                    MessageBox.Show("El bloque " + (i + 1) + " (" + v + " KB) no puede ser mayor a la memoria total (" + total + " KB).",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                lista.Add(v);
            }

            if (lista.Count == 0)
            {
                MessageBox.Show("Escribe al menos un bloque.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            int suma = lista.Sum();
            if (suma > total)
            {
                MessageBox.Show("La suma de los bloques (" + suma + " KB) supera la memoria disponible (" + total + " KB).",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void btnInitMemory_Click(object sender, EventArgs e)
        {
            int totalSize;
            if (!int.TryParse(txtInitialMemory.Text, out totalSize) || totalSize <= 0)
            {
                MessageBox.Show("Por favor, ingrese un tamaño de memoria válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<int> lista;
            if (!LeerBloques(totalSize, out lista)) return;

            memoryBlocks.Clear();
            processList.Clear();
            blockIdCounter = 1;

            int dir = 0;
            foreach (int tam in lista)
            {
                memoryBlocks.Add(new MemoryBlock
                {
                    Id = blockIdCounter++,
                    StartAddress = dir,
                    Size = tam,
                    UsedSize = 0,
                    IsFree = true,
                    ProcessName = null
                });
                dir += tam;
            }

            ActualizarVista();
            MessageBox.Show("Memoria inicializada: " + lista.Count + " bloque(s), " + lista.Sum() + " KB de " + totalSize + " KB.",
                "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ====== Asignar (nucleo con los 3 algoritmos) ======
        private bool AsignarProceso(string procName, int procSize, out string error)
        {
            error = null;

            if (memoryBlocks.Count == 0)
            {
                error = "Primero inicia la memoria.";
                return false;
            }

            var freeBlocks = memoryBlocks.Where(b => b.IsFree && b.Size >= procSize).ToList();
            if (freeBlocks.Count == 0)
            {
                error = "No hay un bloque libre donde quepa el proceso (" + procSize + " KB).";
                return false;
            }

            MemoryBlock selectedBlock = null;
            string estrategia = cmbEstrategia.SelectedItem?.ToString() ?? "Mejor Ajuste (Best Fit)";

            if (estrategia.Contains("Primer Ajuste"))
                selectedBlock = freeBlocks.First();                                   // el primero que cabe
            else if (estrategia.Contains("Mejor Ajuste"))
                selectedBlock = freeBlocks.OrderBy(b => b.Size).First();              // el mas pequeno que cabe
            else
                selectedBlock = freeBlocks.OrderByDescending(b => b.Size).First();    // el mas grande

            // Particion fija: el proceso ocupa todo el bloque (lo que sobra es fragmentacion interna)
            selectedBlock.IsFree = false;
            selectedBlock.ProcessName = procName;
            selectedBlock.UsedSize = procSize;

            processList.Add(new ProcessRecord { Name = procName, Size = procSize, Status = "Asignado" });
            return true;
        }

        private void btnAllocate_Click(object sender, EventArgs e)
        {
            string procName = txtProcessName.Text.Trim();
            if (string.IsNullOrEmpty(procName))
            {
                MessageBox.Show("Ingrese un nombre para el proceso.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (processList.Any(p => p.Name == procName && p.Status == "Asignado"))
            {
                MessageBox.Show("El proceso '" + procName + "' ya se encuentra activo en memoria.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int procSize;
            if (!int.TryParse(txtProcessSize.Text, out procSize) || procSize <= 0)
            {
                MessageBox.Show("Ingrese un tamaño de proceso válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string error;
            if (!AsignarProceso(procName, procSize, out error))
            {
                MessageBox.Show(error, "Memoria Insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ActualizarVista();
            txtProcessName.Clear();
            txtProcessSize.Clear();
            txtProcessName.Focus();
        }

        // ====== Liberar ======
        private void LiberarBloque(MemoryBlock block, bool marcarFinalizado)
        {
            string nombre = block.ProcessName;
            block.IsFree = true;
            block.ProcessName = null;
            block.UsedSize = 0;

            if (marcarFinalizado)
            {
                var rec = processList.FirstOrDefault(p => p.Name == nombre && p.Status == "Asignado");
                if (rec != null) rec.Status = "Finalizado";
            }
            else
            {
                processList.RemoveAll(p => p.Name == nombre && p.Status == "Asignado");
            }
        }

        private void btnDeallocate_Click(object sender, EventArgs e)
        {
            if (dgvMemory.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione el bloque que desea liberar (en la tabla o haciendo clic en la gráfica).",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedIndex = dgvMemory.SelectedRows[0].Index;
            if (selectedIndex < 0 || selectedIndex >= memoryBlocks.Count) return;

            MemoryBlock block = memoryBlocks[selectedIndex];
            if (block.IsFree)
            {
                MessageBox.Show("El bloque seleccionado ya se encuentra libre.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            LiberarBloque(block, true);
            ActualizarVista();
        }

        // ====== Modificar proceso (cambiar su tamaño) ======
        // Libera el proceso seleccionado y lo vuelve a asignar con el nuevo tamaño
        // usando la estrategia elegida. Si no cabe, todo queda como estaba.
        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvMemory.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione el bloque ocupado que desea modificar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int idx = dgvMemory.SelectedRows[0].Index;
            if (idx < 0 || idx >= memoryBlocks.Count) return;

            MemoryBlock block = memoryBlocks[idx];
            if (block.IsFree)
            {
                MessageBox.Show("Seleccione un bloque OCUPADO (el que tiene un proceso).", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int nuevoTam;
            if (!int.TryParse(txtProcessSize.Text, out nuevoTam) || nuevoTam <= 0)
            {
                MessageBox.Show("Escriba en la casilla 'Tamaño' el nuevo tamaño del proceso.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nombre = block.ProcessName;

            // Copia de seguridad por si no cabe con el nuevo tamaño
            var bloquesCopia = memoryBlocks.Select(b => b.Clone()).ToList();
            var procesosCopia = processList.Select(p => p.Clone()).ToList();
            int idCopia = blockIdCounter;

            LiberarBloque(block, false);

            string error;
            if (!AsignarProceso(nombre, nuevoTam, out error))
            {
                memoryBlocks = bloquesCopia;
                processList = procesosCopia;
                blockIdCounter = idCopia;
                ActualizarVista();
                MessageBox.Show("No se pudo modificar '" + nombre + "': " + error + "\nTodo quedó como estaba.",
                    "Memoria Insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            txtProcessSize.Clear();
            ActualizarVista();
        }

        // ====== Reiniciar ======
        private void btnReset_Click(object sender, EventArgs e)
        {
            memoryBlocks.Clear();
            processList.Clear();
            blockIdCounter = 1;

            txtInitialMemory.Clear();
            txtBloques.Clear();
            txtProcessName.Clear();
            txtProcessSize.Clear();

            ActualizarVista();
            MessageBox.Show("Simulador reiniciado. Ingrese una nueva memoria total y elija otra estrategia.",
                "Reinicio", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ====== Grafica de la memoria ======
        private void ConfigurarGrafica()
        {
            // Evita el parpadeo del dibujo
            typeof(Control).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(pnlGrafica, true, null);
            pnlGrafica.BackColor = Color.White;

            pnlGrafica.Paint -= pnlGrafica_Paint; pnlGrafica.Paint += pnlGrafica_Paint;
            pnlGrafica.MouseClick -= pnlGrafica_MouseClick; pnlGrafica.MouseClick += pnlGrafica_MouseClick;
            pnlGrafica.Resize += (s, e) => pnlGrafica.Invalidate();
            dgvMemory.SelectionChanged += (s, e) => pnlGrafica.Invalidate();
        }

        private void pnlGrafica_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(pnlGrafica.BackColor);
            rectBloques.Clear();

            using (Font fTitulo = new Font("Segoe UI", 8.5f))
            using (Font fBloque = new Font("Segoe UI", 8f, FontStyle.Bold))
            using (Font fDir = new Font("Segoe UI", 7.5f))
            {
                if (memoryBlocks.Count == 0)
                {
                    g.DrawString("Inicia la memoria para ver la gráfica.", fTitulo, Brushes.Gray, 8, 8);
                    return;
                }

                int margen = 8;
                int ancho = pnlGrafica.ClientSize.Width - 2 * margen;
                int alto = 60;
                int y = 28;
                int total = memoryBlocks.Sum(b => b.Size);

                int fragTotal = memoryBlocks.Where(b => !b.IsFree).Sum(b => b.Size - b.UsedSize);
                int libreTotal = memoryBlocks.Where(b => b.IsFree).Sum(b => b.Size);
                g.DrawString("Total " + total + " KB  |  Frag. interna " + fragTotal + " KB  |  Libre " + libreTotal +
                    " KB  |  Verde=libre  Rojo=usado  Gris=frag.", fTitulo, Brushes.Black, margen, 6);

                int selIndex = dgvMemory.SelectedRows.Count > 0 ? dgvMemory.SelectedRows[0].Index : -1;

                using (Brush libre = new SolidBrush(Color.LightGreen))
                using (Brush sobrante = new SolidBrush(Color.Gainsboro))
                using (Brush usado = new SolidBrush(Color.LightCoral))
                using (Pen borde = new Pen(Color.Black))
                using (Pen seleccion = new Pen(Color.RoyalBlue, 3))
                using (StringFormat centro = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                {
                    float x = margen;
                    for (int i = 0; i < memoryBlocks.Count; i++)
                    {
                        MemoryBlock b = memoryBlocks[i];
                        float w = (float)ancho * b.Size / total;
                        RectangleF r = new RectangleF(x, y, w, alto);
                        rectBloques.Add(r);

                        if (b.IsFree)
                        {
                            g.FillRectangle(libre, r);
                        }
                        else
                        {
                            g.FillRectangle(sobrante, r);
                            g.FillRectangle(usado, x, y, w * b.UsedSize / b.Size, alto);
                        }

                        g.DrawRectangle(borde, r.X, r.Y, r.Width, r.Height);
                        if (i == selIndex)
                            g.DrawRectangle(seleccion, r.X, r.Y, r.Width, r.Height);

                        if (w > 30)
                        {
                            g.SetClip(r);
                            string txt = (b.IsFree ? "Libre" : b.ProcessName) + "\n" + b.Size + " KB";
                            g.DrawString(txt, fBloque, Brushes.Black, r, centro);
                            g.ResetClip();

                            g.DrawString(b.StartAddress.ToString(), fDir, Brushes.DimGray, x, y + alto + 2);
                        }
                        x += w;
                    }
                }
            }
        }

        // Al hacer clic en un bloque de la grafica se selecciona su fila en la tabla
        private void pnlGrafica_MouseClick(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < rectBloques.Count; i++)
            {
                if (rectBloques[i].Contains(e.Location) && i < dgvMemory.Rows.Count)
                {
                    dgvMemory.ClearSelection();
                    dgvMemory.CurrentCell = dgvMemory.Rows[i].Cells[0];
                    dgvMemory.Rows[i].Selected = true;
                    break;
                }
            }
        }
    }
}