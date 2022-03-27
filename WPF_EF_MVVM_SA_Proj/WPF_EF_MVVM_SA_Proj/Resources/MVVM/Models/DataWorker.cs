using WPF_EF_MVVM_SA_Proj.Resources.MVVM.Models.Data;
using System.Collections.Generic;
using System.Linq;
using System;

namespace WPF_EF_MVVM_SA_Proj.Resources.MVVM.Models
{
    public static class DataWorker
    {
        //получить всех студентов
        public static List<Student> GetAllStudents()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Students.ToList();
                return result;
            }
        }
        //получить все оценки
        public static List<Grade> GetAllGrades()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Grades.ToList();
                return result;
            }
        }
        //получить все Дисциплины
        public static List<Discipline> GetAllDisciplines()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Disciplines.ToList();
                return result;
            }
        }
        //получить все группы
        public static List<Group> GetAllGroups()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Groups.ToList();
                return result;
            }
        }
        //создать Студента
        public static string CreateStudent(string firstName,Group group)
        {
            string result = "Уже существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                //проверяем сущесвует ли студент
                bool checkIsExist = db.Students.Any(el => el.StudentFIO == firstName);
                if (!checkIsExist)
                {
                    Student newStudent = new Student
                    {
                        StudentFIO = firstName,
                        GroupId=group.Id
                    };
                    db.Students.Add(newStudent);
                    db.SaveChanges();
                    result = "Студент добавлен!";
                }
                return result;
            }
        }
        //создать Группу
        public static string CreateGroup(string groupName,int course)
        {
            string result = "Уже существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                //проверяем сущесвует ли группа
                bool checkIsExist = db.Groups.Any(el => el.GroupName == groupName);
                if (!checkIsExist)
                {
                    Group newGroup = new Group
                    {
                        GroupName = groupName,
                        Course = course
                    };
                    db.Groups.Add(newGroup);
                    db.SaveChanges();
                    result = "Группа добавлена!";
                }
                return result;
            }
        }
        //содать Оценку
        public static string CreateGrade(int grade,DateTime date, Discipline discipline,Student student)
        {
            string result = "Уже существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                //проверяем сущесвует ли позиция
                //bool checkIsExist = db.Positions.Any(el => el.Name == name && el.Salary == salary);
                Grade newGrade = new Grade
                {
                    StudentId = student.Id,
                    DisciplineId = discipline.Id,
                    GradeValue = grade,
                    Date = date
                };
                db.Grades.Add(newGrade);
                db.SaveChanges();
                result = "Оценка добавлена!";
                return result;
            }
        }
        //создать Дисчиплину
        public static string CreateDiscipline(string disciplineName)
        {
            string result = "Уже существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                //check the user is exist
                bool checkIsExist = db.Disciplines.Any(el => el.DisciplineName==disciplineName);
                if (!checkIsExist)
                {
                    Discipline newDiscipline = new Discipline
                    {
                        DisciplineName = disciplineName,
                    };
                    db.Disciplines.Add(newDiscipline);
                    db.SaveChanges();
                    result = "Сделано!";
                }
                return result;
            }
        }
        //создать пользователя
        public static bool CheckAuthUser(string login,string password)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return db.Registers.Any(el => el.Password == password && el.Login == login);
            }
        }
        public static string CreateUser(string login,string password)
        {
            string result = "Уже существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                //check the user is exist
                bool checkIsExist = db.Registers.Any(el => el.Password == password && el.Login==login );
                if (!checkIsExist)
                {
                    Register newUser = new Register
                    {
                        Login = login,
                        Password = password,
                    };
                    db.Registers.Add(newUser);
                    db.SaveChanges();
                    result = "Пользователь добавлен!";
                }
                return result;
            }
        }
        //удаление Оценку
        public static string DeleteGrade(Grade grade)
        {
            string result = "Такой оценки не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Grades.Remove(grade);
                db.SaveChanges();
                result = $"Сделано! Оценка {grade.GradeValue} студента {grade.StudentId} удалена";
            }
            return result;
        }
        //удаление Студента
        public static string DeleteStudent(Student student)
        {
            string result = "Такого студента не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Students.Remove(student);
                db.SaveChanges();
                result = $"Сделано! Студент {student.StudentFIO} из группы {student.GroupId} удален";
            }
            return result;
        }
        //удаление Дисциплины
        public static string DeleteDiscipline(Discipline discipline)
        {
            string result = "Такой дисциплины не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                //check position is exist
                db.Disciplines.Remove(discipline);
                db.SaveChanges();
                result = "Сделано! Дисциплина " + discipline.DisciplineName + " удалена";
            }
            return result;
        }
        //удаление Группы
        public static string DeleteGroup(Group group)
        {
            string result = "Такой группы не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                //check position is exist
                db.Groups.Remove(group);
                db.SaveChanges();
                result = "Сделано! Группа " + group.GroupName + " удалена";
            }
            return result;
        }

        //удаление Пользователя
        //public static string DeleteUser(User user)
        //{
        //    string result = "Такого сотрудника не существует";
        //    using (ApplicationContext db = new ApplicationContext())
        //    {
        //        db.Users.Remove(user);
        //        db.SaveChanges();
        //        result = "Сделано! Сотрудник " + user.Name + " уволен";
        //    }
        //    return result;
        //}

        //редактирование Группы
        public static string EditGroup(Group oldGroup, string newName, int newCourse)
        {
            string result = "Такой группы не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                Group group = db.Groups.FirstOrDefault(group => group.Id == oldGroup.Id);
                group.GroupName = newName;
                group.Course = newCourse;
                db.SaveChanges();
                result = "Сделано! Группа " + group.GroupName + " изменена";
            }
            return result;
        }

        //редактирование Студента
        public static string EditStudent(Student oldStudent, string newFirstName, string newLastName, string newMiddleName,Group newGroup)
        {
            string result = "Такого студента не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                Student student = db.Students.FirstOrDefault(student => student.Id == oldStudent.Id);
                student.StudentFIO = newFirstName;
                student.GroupId = newGroup.Id;
                db.SaveChanges();
                result =  $"Сделано! Студент {student.StudentFIO} из группы {student.GroupId} изменен";
            }
            return result;
        }
        //редактирование Дисиплины
        public static string EditDiscipline(Discipline oldDiscipline, string newDisciplineName)
        {
            string result = "Такой дисциплины не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                Discipline discipline = db.Disciplines.FirstOrDefault(discipline => discipline.Id == oldDiscipline.Id);
                discipline.DisciplineName = newDisciplineName;
                db.SaveChanges();
                result = $"Сделано! Дисциплина {discipline.DisciplineName} изменена";
            }
            return result;
        }
        //редактирование Оценки
        public static string EditGrade(Grade oldGrade, Student newStudent, Discipline newDiscipline, int newGradeValue,DateTime newDate)
        {
            string result = "Такой оценки не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                //check user is exist
                Grade grade = db.Grades.FirstOrDefault(grade => grade.Id == oldGrade.Id);
                if (grade != null)
                {
                    grade.StudentId = newStudent.Id;
                    grade.DisciplineId = newDiscipline.Id;
                    grade.GradeValue = newGradeValue;
                    grade.Date = newDate;
                    db.SaveChanges();
                    result = $"Сделано! Оценка {grade.GradeValue} студента {grade.StudentId} изменен";
                }
            }
            return result;
        }
        //получение студента по id студента
        public static Student GetStudentById(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Student student = db.Students.FirstOrDefault(st => st.Id == id);
                return student;
            }
        }
        //получение дисциплины по id дисциплины
        public static Discipline GetDisciplineById(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Discipline discipline = db.Disciplines.FirstOrDefault(dis => dis.Id == id);
                return discipline;
            }
        }
        //получение оценки по id оценки
        public static Grade GetGradeById(int id)
        {
            using(ApplicationContext db = new ApplicationContext())
            {
                Grade grade = db.Grades.FirstOrDefault(grd => grd.Id == id);
                return grade;
            }
        }
        //получение группы по id группы
        public static Group GetGroupById(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Group group = db.Groups.FirstOrDefault(grp => grp.Id == id);
                return group;
            }
        }
        //получение всех студентов по id группы
        public static List<Student> GetAllStudentsByGroupId(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Student> students = (from student in GetAllStudents() where student.GroupId == id select student).ToList();
                return students;
            }
        }
        //получение всех оценок по id дисциплины
        public static List<Grade> GetAllGradesByDisciplineId(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Grade> grades = (from grade in GetAllGrades() where grade.DisciplineId == id select grade).ToList();
                return grades;
            }
        }
        //получение всех оценок по id студента
        public static List<Grade> GetAllGradesByStudentId(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Grade> grades = (from grade in GetAllGrades() where grade.StudentId == id select grade).ToList();
                return grades;
            }
        }
        //получение всех оценок по id студента
        public static List<Grade> GetAllGradesByStudentAndDisciplineId(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Grade> grades = (from grade in GetAllGrades() where grade.StudentId == id && grade.DisciplineId == id select grade).ToList();
                return grades;
            }
        }
    }
}
