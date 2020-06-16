changelog:
1. Dodałem pętle foreach, które rozwiązują problem deklarowania wszystkich labeli jako "hidden" i zastępują 20 linijek kodu 5. :) 
2. Obsłużyłem wyjątki przy dodawaniu danych do bazy. 
3. Usprawniłem mechanizm dodawania danych do bazy. 
4. Dodałem funkcję przeglądania danych. 
5. Dodałem wstępny podział na użytkowników - diagnoste, admina i użytkownika. 
6. Wpisanie błędnych danych powoduje pojawienie się ErrorWindow lub ErrorWindow2. 
7. Dodałem przycisk czyszczenia bazy danych. 

problemy: 

MainWindow
	1. Backgroundworker robi niezłe jaja, bo po zamknięciu aplikacji krzyżykiem, Visual Studio dalej uważa, że jest uruchomiona (debugowanie wciąż trwa) i faktyczne zamknięcie aplikacji odbywa się dopiero po zatrzymaniu debuggowania w Visual Studio.
SecondMainWindow
	2. Logowanie nie działa, nie wiem jak je naprawić. 
	BrowseData
3. Przy wyświetlaniu danych nie wyświetlają się daty (nie wiem czemu). 
	BrowseData
4. Przy wyświetlaniu danych nie wiem, jak wyświetlać dane z łączenia tabel (JOIN w query).

