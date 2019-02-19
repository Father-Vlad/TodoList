﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TodoList.Core.Interfaces;
using TodoList.Core.Models;

namespace TodoList.Core.Services
{
    public class WebApiService : IWebApiService
    {
        private IGoalService _goalService;
        private HttpClient _client;
        private ILoginService _loginService;
        private readonly string _addressURL = "http://10.10.3.207:49780/api/values/";

        public WebApiService(IGoalService goalService, ILoginService loginService)
        {
            _client = new HttpClient();
            _goalService = goalService;
            _loginService = loginService;
        }

        public async Task InsertOrUpdateDataAsync(Goal goal)
        {
            var uri = new Uri(string.Format(_addressURL));
            var json = JsonConvert.SerializeObject(goal);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            if (goal.Id != 0)
            {
                response = await _client.PutAsync(uri, content);
            }
            if (goal.Id == 0)
            {
                response = await _client.PostAsync(uri, content);
            }
            if (response.IsSuccessStatusCode)
            {
                _goalService.InsertGoal(goal);
            }
        }

        public async Task DeleteDataAsync(int id)
        {
            var uri = new Uri(string.Format(_addressURL + id.ToString()));
            var response = await _client.DeleteAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                _goalService.DeleteGoal(id);
            }
        }

        public async Task<List<Goal>> RefreshDataAsync()
        {
            List<Goal> goals = null;
            var currentUserId = _loginService.CurrentUserId;
            var uri = new Uri(string.Format(_addressURL + currentUserId));
            var response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                goals = JsonConvert.DeserializeObject<List<Goal>>(content);
                _goalService.DeleteAllUserGoals(currentUserId);
                _goalService.InsertAllUserGoals(goals);
            }
            return goals;
        }
    }
}