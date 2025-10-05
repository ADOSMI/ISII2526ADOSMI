﻿using System;
namespace AppForSEII2526.API.Models
{
    [PrimaryKey(nameof(BocadilloId), nameof(CompraId))]
    public class CompraBocadillo
    {
        public CompraBocadillo()
        {

        }

        public CompraBocadillo(Bocadillo bocadillo, int cantidad, Compra compra)
        {
            Bocadillo = bocadillo;
            BocadilloId = bocadillo.Id;
            Compra = compra;
            CompraId = compra.Id;
            Cantidad = cantidad;
            Precio = bocadillo.PVP;
            NombreBocadillo = bocadillo.Nombre;
            TipoPan = bocadillo.TipoPan;
        }

        public Bocadillo Bocadillo { get; set; }

        public int BocadilloId { get; set; }

        public Compra Compra { get; set; }

        public int CompraId { get; set; }

        public int Cantidad { get; set; }

        [Precision(10, 2)]
        public decimal Precio { get; set; }

        public string NombreBocadillo { get; set; }

        public TipoPan TipoPan { get; set; }
    }
}