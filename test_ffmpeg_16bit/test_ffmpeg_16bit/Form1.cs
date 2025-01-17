﻿using BitMiracle.LibTiff.Classic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_ffmpeg_16bit
{
    public partial class Form1 : Form
    {
        List<Bitmap> imgs;
        public Form1()
        {
            InitializeComponent();

        }

        private void button_load_Click(object sender, EventArgs e)
        {

            FfmpegVideoLoader ffmpegVideoLoader = new FfmpegVideoLoader(textBox_pathLoad.Text);

            ffmpegVideoLoader.loadMetadata();

           

            //

            if (comboBox_type.Text == "rgb24")
            {
                imgs = ffmpegVideoLoader.loadVideo("rgb24", PixelFormat.Format24bppRgb, 3);
            }
            else if (comboBox_type.Text == "gray16le")
            {

                imgs = ffmpegVideoLoader.loadVideo("gray16", PixelFormat.Format16bppGrayScale, 2);
            }
            else if (comboBox_type.Text == "gray8")
            {

                imgs = ffmpegVideoLoader.loadVideo("gray8", PixelFormat.Format8bppIndexed, 1);
            }

            else
            {
                throw new Exception("invalid selection type");
            }


        }

        private void button_save_Click(object sender, EventArgs e)
        {

            FfmpegVideoWriter ffmpegVideoWriter = new FfmpegVideoWriter(textBox_pathSave.Text);

            if (comboBox_type.Text == "rgb24")
            {
                ffmpegVideoWriter.writeVideo(imgs, 25, "rgb24", "bgr0", imgs.ElementAt(0).Width, imgs.ElementAt(0).Height);
            }
            else if (comboBox_type.Text == "gray16le")
            {

                ffmpegVideoWriter.writeVideo(imgs, 25, "gray16", "gray16le", imgs.ElementAt(0).Width, imgs.ElementAt(0).Height);
            }
            else if (comboBox_type.Text == "gray8")
            {
                ffmpegVideoWriter.writeVideo(imgs, 25, "gray8", "gray8", imgs.ElementAt(0).Width, imgs.ElementAt(0).Height);
            }
            else
            {
                throw new Exception("invalid selection type");
            }
             


        }

        private void button_show_Click(object sender, EventArgs e)
        {
            Bitmap img = imgs.ElementAt(decimal.ToInt32(numericUpDown_frameNum.Value));

            // cant show or convert 16 bit - but you can save them

            //Bitmap tmp_img = new Bitmap(img);
            //Bitmap img_new = tmp_img.Clone(new Rectangle(0, 0, img.Width, img.Height), PixelFormat.Format24bppRgb);
            //pictureBox1.Image = img_new;


            //Bitmap img_new = new Bitmap(img.Width, img.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            //using (Graphics gr = Graphics.FromImage(img_new))
            //{
            //    gr.DrawImage(img, new Rectangle(0, 0, img_new.Width, img_new.Height));
            //}
            //pictureBox1.Image = img_new;

            pictureBox1.Image = img;
        }

        private void numericUpDown_frameNum_ValueChanged(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                button_show.PerformClick();
            }
        }

        private void button_writeTiff_Click(object sender, EventArgs e)
        {

            Bitmap img = imgs.ElementAt(decimal.ToInt32(numericUpDown_frameNum.Value));

            TiffWriter tiffWriter = new TiffWriter();

            tiffWriter.writeTiff(img, numericUpDown_frameNum.Value.ToString() + ".tiff");


        }

        private void textBox_pathSave_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
