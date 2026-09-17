# world-builder

##	Sistemos paskirtis
Projekto tikslas – palengvinti RPG stalo žaidimų vedėjams kūrybinį pasaulio vystymo procesą.
Veikimo principas – platformą sudaro dvi dalys: internetinė svetainė, kuria galės naudotis vedėjai, žaidėjai bei administratorius bei serverinė dalis, atsakinga už duomenų apdorojimą, kaupimą.
Naudotojas, norintis naudotis platforma, užsiregistruos ir galės kurti savo pasaulius, kiekvienam iš tų pasaulių gyvenvietes, o gyvenvietes užpildyti norimais veikėjais. Žaidėjai, dalyvaujantys vedėjo kampanijoje, taip pat galės prisijungti prie svetainės ir ten galės prisijungti prie esančių kampanijų arba kurti savas (taip patys tapdami vedėjais). Administratorius pats negalėtų kurti jokių kampanijų, tačiau jis matytu visą informaciją tad galėtų šalinti, keisti naudojimosi gairių neatitinkančius pasaulius, gyvenvietes, veikėjus.
##	Funkciniai reikalavimai
Svečias galės:
1.	Peržiūrėti viešų pasaulių aprašymus
2.	Prisijungti prie aplikacijos
Žaidėjas galės:
1.	Peržiūrėti kitų žaidėjų sukurtus viešus pasaulius
2.	Peržiūrėti privačius pasaulius, prie kurių jis yra pridėtas
3.	Atsijungti
4.	Sukurti savo pasaulį
5.	Savo pasauliams sukurti gyvenvietes
6.	Savo miestams sukurti veikėjus
Administratorius galės:
1.	Ištrinti bet kokius pasaulius ar jų sudedamąsias
2.	Redaguoti bet kokius pasaulius ar jų sudedamąsias
3.	Šalinti naudotojus
4.	Peržiūrėti naudotojų sąrašą
##	SISTEMOS ARCHITEKTŪRA
Sistema susideda iš:
-	Frontend – Next.js
-	Backend – ASP.NET kartu su SQL Server duomenų baze
