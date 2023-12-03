﻿using City_Transportation_Systems.Data;
using City_Transportation_Systems.Interfaces;
using City_Transportation_Systems.Models;
using Microsoft.EntityFrameworkCore;

namespace City_Transportation_Systems.Repository
{
    public class ScheduleRepository : IScheduleRepository
    {
        private CtsDbContext _db;
        public ScheduleRepository(CtsDbContext db)
        {
            _db = db;
        }

        public async Task<bool> CreateScheduleAsync(Schedule schedule)
        {
            await _db.AddAsync(schedule);
            return await SaveChanges();
        }

        public async Task<bool> DeleteScheduleAsync(Schedule schedule)
        {
            _db.Remove(schedule);
            return await SaveChanges();
        }

        public async Task<IEnumerable<Schedule>> GetFullScheduleAsync()
        {
            var schedule = await _db.Schedules.ToListAsync();
            return schedule;
        }

        public async Task<Schedule> GetScheduleByIdAsync(int id)
        {
            var schedule = await _db.Schedules.FirstOrDefaultAsync(s => s.Id == id);
            return schedule;
        }

        public async Task<IEnumerable<Schedule>> GetSchedulesByRoute(int RouteId)
        {
            var schedules = await _db.Schedules.Where(s => s.RouteId == RouteId).ToListAsync();
            return schedules;
        }

        public async Task<IEnumerable<Schedule>> GetSchedulesByStation(int StationId)
        {
            var schedules = await _db.Schedules.Where(s => s.StationId == StationId).ToListAsync();
            return schedules;
        }

        public async Task<IEnumerable<Schedule>> GetSchedulesByTimeAndRoute(TimeSpan time, int RouteId)
        {
             var schedules = await _db.Schedules
            .Where(schedule => schedule.RouteId == RouteId && schedule.TimeStamp > time)
            .OrderBy(schedule => schedule.TimeStamp)
            .ToListAsync();

            return schedules;
        }

        public async Task<IEnumerable<Schedule>> GetSchedulesByTimeAndStation(TimeSpan time, int StationId)
        {
            var schedules = await _db.Schedules
           .Where(schedule => schedule.StationId == StationId && schedule.TimeStamp > time)
           .OrderBy(schedule => schedule.TimeStamp)
           .ToListAsync();

            return schedules;
        }

        public async Task<bool> UpdateScheduleAsync(Schedule schedule)
        {
           _db.Update(schedule);
            return await SaveChanges();
        }

        private async Task<bool> SaveChanges()
        {
            var isSaved = await _db.SaveChangesAsync();
            return isSaved > 0;
        }
    }
}
