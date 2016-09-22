swde.dll v1.0.1, 3 kwietnia 2014
---
Biblioteka do odczytu formatu SWDE 2.0

# Klasy

Główne wzorce projektowe kompozyt, wizytator, budowniczy, obserwator.  
Prosta fabryka, metoda szablonowa.  
Zastosowano wzorzec fasada do uproszczenia złożonego modelu pliku SWDE.  
Większość niskopoziomowych klas modelu SWDE otrzymała poziom widoczności internal,  
w zestawie udostępniono publicznie mniejszą ilość prostszych w użyciu klas wyższego poziomu.

Zrezygnowano ze stworzenia pełnego interfejsu G5 pośredniczącego w dostępnie do ogólnego modelu SWDE.
Zamiast tego jest bardziej ogólny model z możliwością określenia wersji SWDE (Ogólna, 2.0 lub 3.0).

## FabrykaKomponentow

Przekształca/obiektuje reprezentację tekstową elementów pliku swde na obiekty.
Symbol reprezentuje pojedynczą linię w pliku swde oraz posiada atrybuty opisujące tą linię.
Konstruowane komponenty przekazywane są do budowniczego, który z przekazywanych komponentów
tworzy bardziej złożone obiekty oraz analizuje poprawność kontekstu dla każdego symbolu.
 
## KomponentBase

## BudowniczySwde

## SwdeReader

## DokumentSwde

 Dostęp, wyszukiwanie obiektów pliku SWDE reprezentowanych przez klasę ObiektSwde.

## ObiektSwde

Ogólny obiekt pliku SWDE, pośredniczy w dostępnie do RekordSwdeBase. Dostęp do atrybutów, relacji, obiektów powiązanych.

## GeometriaSwde

Reprezentacja geometrii obiektu SWDE w postacji WKT.

# Historia

Do zrobienia

2014-04-03 v1.0.1

* aktualizacja: możliwość odczytania/iteracji wszystkich atrybutów obiektu SWDE
* poprawka: pomijanie archiwalnych obiektów

2014-01-27 v1.0

* nowa funkcja: możliwość określenia zapisu współrzędnych w pliku SWDE: geodezyjny czy matematyczny (dotyczy geometrii obiektów)
