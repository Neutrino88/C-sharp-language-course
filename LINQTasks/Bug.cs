namespace LINQTasks {
    public class Bug {
        public string Info { get; }
        public User CreatedBy { get; }
        public User AssignedTo { get; private set; }
        public Status Status { get; private set; }
        public Priority Priority { get; }

        internal Bug(string info, User createdBy, Priority priority) {
            Info = info;
            CreatedBy = createdBy;
            Priority = priority;
            Status = Status.Created;
        }

        public void Modify(User assignedTo, Status status) {
            AssignedTo = assignedTo;
            Status = status;
        }

        public override string ToString() {
            return $"{Info} Status:{Status} Priority:{Priority} AssignedTo:{AssignedTo}";
        }
    }
}