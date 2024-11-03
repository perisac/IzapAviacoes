﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Topicos3.Models
{
    public class Comissario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [StringLength(14, ErrorMessage = "O CPF deve ter no máximo 14 caracteres.")]
        public string Cpf { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Os anos de experiência devem ser um valor positivo.")]
        public int AnosExperiencia { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "A data do último treinamento é obrigatória.")]
        public DateTime UltimoTreinamento { get; set; }
    }
}