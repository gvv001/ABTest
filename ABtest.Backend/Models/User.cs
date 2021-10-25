using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABtest
{
    public class User
    {


        /// <summary>
        /// Уникальный ID пользователя
        /// </summary>
        [Key]
        public int UserID { get; set; }


        /// <summary>
        /// Дата регистрации
        /// </summary>
        

        [DataType(DataType.Date)]
        [Required]
        public DateTime DateRegistration { get; set; }


        /// <summary>
        /// Дата последней активности
        /// </summary>
        //[DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Required]
        public DateTime DateLastActivity { get; set; }


        /// <summary>
        /// Время жизни пользователя(кол-во дней)
        /// </summary>
        /// 

        public double LifespanInDays { get; set; }
    }
}
