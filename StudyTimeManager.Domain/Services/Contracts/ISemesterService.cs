﻿using StudyTimeManager.Domain.Models;

namespace StudyTimeManager.Domain.Services.Contracts;

/// <summary>
/// Handles CRUD operations related to a semester
/// </summary>
public interface ISemesterService
{
    /// <summary>
    /// Creates a semester for a student
    /// </summary>
    /// <param name="semester">Semester to be created</param>
    /// <returns><see langword="true"/> if semester is created successfully, <see langword="false"/> if otherwise </returns>
    bool CreateSemester(Semester semester);

    /// <summary>
    /// Retrieves the semester a student is in
    /// </summary>
    /// <returns>The semester is enrolled in</returns>
    Semester GetSemester();

    /// <summary>
    /// Retrieves the number of weeks for the created semester
    /// </summary>
    /// <returns>Number of weeks in semester</returns>
    int GetNumberOfWeeks();
}