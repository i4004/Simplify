using System;
using System.Collections.Generic;
using NCrontab;

namespace Simplify.WindowsServices.Jobs.Crontab
{
	/// <summary>
	/// Represent multiple crontab schedules processor
	/// </summary>
	public interface ICrontabProcessor
	{
		/// <summary>
		/// Gets the schedules.
		/// </summary>
		/// <value>
		/// The schedules.
		/// </value>
		IList<CrontabSchedule> Schedules { get; }

		/// <summary>
		/// Gets the next occurrences.
		/// </summary>
		/// <value>
		/// The next occurrences.
		/// </value>
		IList<DateTime> NextOccurrences { get; }

		/// <summary>
		/// Calculates the next occurrences using current time as base time.
		/// </summary>
		void CalculateNextOccurrences();

		/// <summary>
		/// Calculates the next occurrences.
		/// </summary>
		/// <param name="baseTime">The base time.</param>
		void CalculateNextOccurrences(DateTime baseTime);

		/// <summary>
		/// Determines whether the specified time is matching next occurrence.
		/// </summary>
		/// <param name="time">The time.</param>
		/// <returns></returns>
		bool IsMatching(DateTime time);

		/// <summary>
		/// Determines whether current time is matching next occurrence.
		/// </summary>
		/// <returns></returns>
		bool IsMatching();
	}
}