namespace LINQTasks {
    public class User {
        public string Name { get; }
        public UserType UserType { get; }

        internal User(string name, UserType userType) {
            Name = name;
            UserType = userType;
        }

        public override string ToString() {
            return $"{Name} {UserType}";
        }
    }
}