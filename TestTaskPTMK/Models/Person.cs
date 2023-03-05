using System;
using System.ComponentModel.DataAnnotations;

namespace TestTaskPTMK.Models
{
    public class Person
    {
        [Key]
        public long Id { get; set; }
        [Display(Name = "Фамилия")]
        [MaxLength(127, ErrorMessage = "Max last name length is 127"), Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }
        [Display(Name = "Имя")]
        [MaxLength(127, ErrorMessage = "Max first name length is 127"), Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }
        [Display(Name = "Отчество")]
        [MaxLength(127, ErrorMessage = "Max middle name length is 127"), Required(ErrorMessage = "Middle name is required")]
        public string MiddleName { get; set; }
        [Display(Name = "Дата рождения")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Birth date is required")]
        public DateTime BirthDate { get; set; }
        public Sex Sex { get; set; }
        [Required(ErrorMessage = "SexId pointing is required")]
        public long? SexId { get; set; }
    }
}


/*

create table if not exists sexes(id bigserial not null primary key, name varchar(31) not null unique);

create table if not exists people(id bigserial not null primary key, last_name varchar(127) not null, first_name varchar(127) not null, middle_name varchar(127) not null, birth_date date not null, Sex_Id bigint not null, FOREIGN KEY(Sex_Id) REFERENCES SEXES(Id) ON DELETE CASCADE ON UPDATE CASCADE);

*/

/*
create index person_last_name_index on people(last_name);
create index person_first_name_index on people(first_name);
create index person_middle_name_index on people(middle_name);
create index person_birth_date_index on people(birth_date);
create index person_sex_id_index on people(sex_id);

create index sex_name_index on sexes(name);
*/