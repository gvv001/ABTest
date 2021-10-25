using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABtest
{
    public class User
    {


        /// <summary>
        /// ���������� ID ������������
        /// </summary>
        [Key]
        public int UserID { get; set; }


        /// <summary>
        /// ���� �����������
        /// </summary>
        

        [DataType(DataType.Date)]
        [Required]
        public DateTime DateRegistration { get; set; }


        /// <summary>
        /// ���� ��������� ����������
        /// </summary>
        //[DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Required]
        public DateTime DateLastActivity { get; set; }


        /// <summary>
        /// ����� ����� ������������(���-�� ����)
        /// </summary>
        /// 

        public double LifespanInDays { get; set; }
    }
}
