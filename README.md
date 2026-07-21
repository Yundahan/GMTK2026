Projekt das erste Mal runterladen:

1. im Windows Explorer in den Ordner gehen, in dem das Projekt sein soll -> Rechtsklick -> Git Bash Here
3. folgenden Command ausführen:

	``git clone https://github.com/Yundahan/GMTK2026.git``

5. Unity Hub öffnen -> oben rechts auf Add klicken -> Add project from disk -> von git erstellten Ordner GMTK2026 auswählen

Neuste Updates aus dem Git Repository pullen:

1. in GMTK2026 Ordner gehen -> Rechtsklick -> Git Bash Here
2. folgenden Command ausführen

	``git pull``

Wenn nach ``git pull`` der blaue Text vor dem Command ist ``(master|MERGING)``, statt einfach nur ``(master)``, Fabian oder mich fragen

Wenn nach ``git pull`` das letzte Wort von dem Konsolentext ``Aborting`` ist, dann gibt es lokale Changes, die von ``git pull`` überschrieben werden würden. In dem Fall einmal die Schritte 1. - 4. vom nächsten Abschnitt ausführen und dann nochmal ``git pull`` versuchen

Eigene Changes ins Repository pushen:

1. Einmal in den Unity Editor klicken, damit Unity alle Files nochmal korrekt updated
2. in GMTK2026 Ordner gehen -> Rechtsklick -> Git Bash Here
3. folgenden Command ausführen

	``git status``

Das listet einem alle Files auf, die man angepasst hat. Wenn da irgendwas dabei ist, was einem seltsam vorkommt, Fabian oder mich fragen
4. folgende 2 Commands ausführen

	git add .
	git commit -m "Message einfügen"

5. folgenden Command ausführen
	``git push``
Wenn bei ``git push`` ein Fehler auftritt (macht sich meist durch roten Text in der Konsole bemerkbar), gibt es im Repository vermutlich Changes, die man selber noch nicht hat. Dann muss man einmal

	``git pull``
  
ausführen und danach nochmal ``git push`` versuchen. Auch hier wieder, wenn nach ``git pull`` der blaue Text ``(master|MERGING)`` spawnt, Fabian oder mich fragen
