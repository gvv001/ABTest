using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABtest
{
    public class UserLifeSpan
    {

        /// <summary>
        /// Счётчик количества пользователй
        /// </summary>
        /// 
        [Required]
        public int Count { get; set; }


        /// <summary>
        /// Количество времени в днях от регистрации до последней активности
        /// </summary>
        //[Required]
        public double LifeSpan { get; set; }
        
        
 
    }
}
