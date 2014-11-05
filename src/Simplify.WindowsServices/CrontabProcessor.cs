using System;
using System.Collections.Generic;
using System.Linq;
using NCrontab;
using Simplify.System;

namespace Simplify.WindowsServices
{
	/// <summary>
	/// Providers multiple crontab schedules processor
	/// </summary>
	public class CrontabProcessor : ICrontabProcessor
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CrontabProcessor"/> class.
		/// </summary>
		/// <param name="crontabExpression">The crontab expression.</param>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ServiceInitializationException"></exception>
		public CrontabProcessor(string crontabExpression)
		{
			if (string.IsNullOrEmpty(crontabExpression))
				throw new ArgumentNullException();

			Schedules = new List<CrontabSchedule>();
			NextOccurrences = new List<DateTime>();

			var crontabExpressions = crontabExpression.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

			foreach (var expression in crontabExpressions)
			{
				var schedule = CrontabSchedule.TryParse(expression);

				if (schedule == null)
					throw new ServiceInitializationException(string.Format("Crontab expression parsing failed, expression: '{0}', full: '{1}'",
						expression, crontabExpression));

				Schedules.Add(schedule);
			}
		}

		/// <summary>
		/// Gets the schedules.
		/// </summary>
		/// <value>
		/// The schedules.
		/// </value>
		public IList<CrontabSchedule> Schedules { get; private set; }

		/// <summary>
		/// Gets the next occurrences.
		/// </summary>
		/// <value>
		/// The next occurrences.
		/// </value>
		public IList<DateTime> NextOccurrences { get; private set; }

		/// <summary>
		/// Calculates the next occurrences.
		/// </summary>
		public void CalculateNextOccurrences()
		{
			CalculateNextOccurrences(TimeProvider.Current.Now);
		}

		/// <summary>
		/// Calculates the next occurrences.
		/// </summary>
		/// <param name="baseTime">The base time.</param>
		public void CalculateNextOccurrences(DateTime baseTime)
		{
			NextOccurrences.Clear();

			foreach (var schedule in Schedules)
				NextOccurrences.Add(schedule.GetNextOccurrence(baseTime));
		}

		/// <summary>
		/// Determines whether the specified time is matching next occurrence.
		/// </summary>
		/// <param name="time">The time.</param>
		/// <returns></returns>
		public bool IsMatching(DateTime time)
		{
			return
				NextOccurrences.Any(
					occurrence =>
						time.Year == occurrence.Year && time.Month == occurrence.Month && time.Day == occurrence.Day &&
						time.Hour == occurrence.Hour && time.Minute == occurrence.Minute);
		}

		/// <summary>
		/// Determines whether current time is matching next occurrence.
		/// </summary>
		/// <returns></returns>
		public bool IsMatching()
		{
			return IsMatching(TimeProvider.Current.Now);
		}
	}
}