using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQTasks {
    public class BugTracker {
        private readonly List<Bug> _bugs = new List<Bug>();
        private readonly List<User> _users = new List<User>();

        public IReadOnlyCollection<User> Users => _users;
        public IReadOnlyCollection<Bug> Bugs => _bugs;


        public User CreateUser(string name, UserType userType) {
            var user = new User(name, userType);
            _users.Add(user);
            return user;
        }

        public Bug CreateBug(string info, User createdBy, Priority priority = Priority.Normal) {
            var bug = new Bug(info, createdBy, priority);
            _bugs.Add(bug);
            return bug;
        }

        /// <summary>
        /// Возвращает все открытые ошибки
        /// </summary>
        public IEnumerable<Bug> GetOpenBugs() {
            return this.Bugs.Where(bug => bug.Status != Status.Closed);
        }

        /// <summary>
        /// Возвращает все открытые ошибки с приоритетом не ниже priority
        /// </summary>
        public IEnumerable<Bug> GetOpenBugs(Priority priority) {
            return this.Bugs.Where(bug => bug.Status != Status.Closed && bug.Priority >= priority);
        }

        /// <summary>
        /// Возвращает все ошибки назначенные на определенного пользователя 
        /// </summary>
        public IEnumerable<Bug> GetBugsByUser(User assignedTo) {
            return this.Bugs.Where(bug => bug.AssignedTo == assignedTo);
        }

        /// <summary>
        /// Возвращает ошибки сгруппированные по приоритету
        /// </summary>
        public IEnumerable<IGrouping<Priority, Bug>> GetBugsGroupeByPriority() {
            return this.Bugs.GroupBy(bug => bug.Priority);
        }

        /// <summary>
        /// Возвращается количество ошибок для каждого статуса
        /// </summary>
        public IEnumerable<Tuple<Status, int>> GetBugsCount() {
            return
                from bug in this.Bugs
                group bug by bug.Status into pair
                select new Tuple<Status, int>(pair.Key, pair.Count());
        }

        /// <summary>
        /// Возвращает все ошибки назначенные их создателю
        /// </summary>
        public IEnumerable<Bug> GetBugsAssignedToAuthor() {
            return this.Bugs.Where(bug => bug.AssignedTo == bug.CreatedBy);
        }

        /// <summary>
        /// Возвращает пользователей на которых не назначена ни одна ошибка
        /// </summary>
        public IEnumerable<User> GetFreeUsers() {
            return
                this.Users.GroupJoin(
                    this.Bugs,
                    user => user,
                    bug => bug.AssignedTo,
                    (user, bugs) => new {
                        user = user,
                        bugs = bugs })
                    .Where(pair => pair.bugs.Count() == 0)
                    .Select(pair => pair.user);
        }

        /// <summary>
        /// Возвращает для каждого пользователя список назначенных ему ошибок
        /// Для пользователей на которых не назначено ни одной ошибки возвращается пустой список
        /// </summary>
        public IEnumerable<Tuple<User, IEnumerable<Bug>>> GetUsersBugs() {
            return
                this.Users.GroupJoin(
                    this.Bugs,
                    user => user,
                    bug => bug.AssignedTo,
                    (user, bugs) => new
                    {
                        user = user,
                        bugs = bugs
                    })
                    .Select(pair => new Tuple<User, IEnumerable<Bug>>(pair.user, pair.bugs)); 
        }

        /// <summary>
        /// Возвращает все ошибки отсортированные по статусу и приоритету (в рамках одинакового статуса)
        /// </summary>
        public IEnumerable<Bug> GetSortedBugs() {
            return
                from bug in this.Bugs
                orderby bug.Status, bug.Priority
                select bug;
        }
    }
}