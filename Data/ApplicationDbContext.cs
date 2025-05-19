using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Api.Models;

namespace SchoolManagementSystem.Api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        //composite key for student cocurricular activity
        builder.Entity<StudentCocurricularActivity>()
            .HasKey(sc => new { sc.StudentId, sc.CocurricularActivityId });
        builder.Entity<StudentCocurricularActivity>()
            .HasOne(sc => sc.Student)
            .WithMany(s => s.CocurricularActivities)
            .HasForeignKey(sc => sc.StudentId);
        builder.Entity<StudentCocurricularActivity>()
            .HasOne(sc => sc.CocurricularActivity)
            .WithMany(c => c.Students)
            .HasForeignKey(sc => sc.CocurricularActivityId);

        //composite key for class teacher
        builder.Entity<ClassTeacher>()
            .HasKey(ct => new { ct.ClassId, ct.TeacherId });
        builder.Entity<ClassTeacher>()
            .HasOne(ct => ct.Class)
            .WithMany(c => c.Teachers)
            .HasForeignKey(ct => ct.ClassId);
        builder.Entity<ClassTeacher>()
            .HasOne(ct => ct.Teacher)
            .WithMany(t => t.Classes)
            .HasForeignKey(ct => ct.TeacherId);

        //composite key for teacher subject
        builder.Entity<TeacherSubject>()
            .HasKey(ts => new { ts.TeacherId, ts.SubjectId });
        builder.Entity<TeacherSubject>()
            .HasOne(ts => ts.Teacher)
            .WithMany(t => t.Subjects)
            .HasForeignKey(ts => ts.TeacherId);
        builder.Entity<TeacherSubject>()
            .HasOne(ts => ts.Subject)
            .WithMany(s => s.Teachers)
            .HasForeignKey(ts => ts.SubjectId);
    }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<CurriculumGoverningBody> CurriculumGoverningBodies { get; set; }
    public DbSet<ExamType> ExamTypes { get; set; }
    public DbSet<Curriculum> Curriculums { get; set; }
    public DbSet<CurriculumLevel> CurriculumLevels { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<CurriculumLevelClass> CurriculumLevelClasses { get; set; }
    public DbSet<ClassStream> ClassStreams { get; set; }
    public DbSet<Guardian> Guardians { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<LessonAttendance> LessonAttendances { get; set; }
    public DbSet<Exam> Exams { get; set; }
    public DbSet<SchoolSetting> SchoolSettings { get; set; }
    public DbSet<ExamRegistration> ExamRegistrations { get; set; }
    public DbSet<Assignment> Assignments { get; set; }
    public DbSet<AssignmentResult> AssignmentResults { get; set; }
    public DbSet<CocurricularActivity> CocurricularActivities { get; set; }
    public DbSet<StudentCocurricularActivity> StudentCocurricularActivities { get; set; }
    public DbSet<ClassTeacher> ClassTeachers { get; set; }
    public DbSet<TeacherSubject> TeacherSubjects { get; set; }
    public DbSet<ExamResult> ExamResults { get; set; }
}
