using WPF_StudentsAchievement2.Resources.MVVM.Models.Data;
using System.Collections.Generic;
using System.Linq;
using System;

namespace WPF_StudentsAchievement2.Resources.MVVM.Models
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
        public static string CreateStudent(string firstName, string lastName, string middleName,Group group)
        {
            string result = "Уже существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                //проверяем сущесвует ли студент
                bool checkIsExist = db.Students.Any(el => el.StudentFirstName == firstName && el.StudentLastName == lastName && el.StudentMiddleName == middleName);
                if (!checkIsExist)
                {
                    Student newStudent = new Student
                    {
                        StudentFirstName = firstName,
                        StudentLastName = lastName,
                        StudentMiddleName = middleName,
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
        public static string CreateUser(string login,string passsword)
        {
            string result = "Уже существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                //check the user is exist
                bool checkIsExist = db.Registers.Any(el => el.Password == passsword && el.Login==login );
                if (!checkIsExist)
                {
                    Register newUser = new Register
                    {
                        Login = login,
                        Password = passsword,
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
                result = $"Сделано! Студент {student.StudentLastName} {student.StudentFirstName} {student.StudentMiddleName} из группы {student.GroupId} удален";
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
        public static string DeleteGoup(Group group)
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
                student.StudentFirstName = newFirstName;
                student.StudentLastName = newLastName;
                student.StudentMiddleName = newMiddleName;
                student.GroupId = newGroup.Id;
                db.SaveChanges();
                result =  $"Сделано! Студент {student.StudentLastName} {student.StudentFirstName} {student.StudentMiddleName} из группы {student.GroupId} изменен";
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

        //получение позиции по id позитиции
        public static Position GetPositionById(int id)
        {
            using(ApplicationContext db = new ApplicationContext())
            {
                Position pos = db.Positions.FirstOrDefault(p => p.Id == id);
                return pos;
            }
        }
        //получение отдела по id отдела
        public static Department GetDepartmentById(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Department pos = db.Departments.FirstOrDefault(p => p.Id == id);
                return pos;
            }
        }
        //получение всех пользователей по id позиции
        public static List<User> GetAllUsersByPositionId(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<User> users = (from user in GetAllUsers() where user.PositionId == id select user).ToList();
                return users;
            }
        }
        //получение всех позиций по id отдела
        public static List<Position> GetAllPositionsByDepartmentId(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Position> positions = (from position in GetAllPositions() where position.DepartmentId == id select position).ToList();
                return positions;
            }
        }
    }
}
