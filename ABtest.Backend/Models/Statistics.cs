using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABtest
{
    public class UserLifeSpan
    {

        /// <summary>
        /// ������� ���������� ������������
        /// </summary>
        /// 
        [Required]
        public int Count { get; set; }


        /// <summary>
        /// ���������� ������� � ���� �� ����������� �� ��������� ����������
        /// </summary>
        //[Required]
        public double LifeSpan { get; set; }
        
        
 
    }
}
