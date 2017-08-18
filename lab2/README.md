## Лабораторная работа №2

1. Написать тесты для всех методов классов из предыдущего задания (`библиотека`, `книга`, `абонент`) используя `NUnit` или `MSTest`.

2. Написать консольное приложение для запуска тестов из пункта 1. Приложение должно запускать все тесты из сборки и выводить список прошедших успешно и упавших. Для упавших должно выводиться сообщение об ошибке.

    * Если в пункте 1 использовался `NUnit`, то приложение должно корректно работать с атрибутами `TestFixture`, `Test`, `SetUp`, `TearDown`.
    
    * Если в пункте 1 использовался `MSTest` то приложение должно корректно работать с атрибутами `TestClass`, `TestMethod`, `TestInitialize`, `TestCleanup`.

3. **_(Дополнительное)_** Написать свою упрощенную библиотеку для тестов и реализовать на основе нее пункты 1 и 2.