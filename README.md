# Spinewise smart chair - FIT Coding Challenge 2024
 ![4](https://github.com/ArminDjidelija/fitcc24-spinewise/assets/110191710/d64ccbef-1b92-4216-9100-e373ac3cdc5b)

# Spinewise Pametna Stolica

## Prezentacija
[Kliknite ovdje za prezentaciju](https://bit.ly/spinewise-fitcc-prezentacija)

## O projektu

Moderni način života uključuje svakodnevno sjedenje satima, bilo to za stolom, pred kompjuterom, tokom gledanja televizije, ili igranja igrica kod kuće. Posljedice toga su problemi sa posturom, deformacije kičme (skolioza), i mnogi drugi problemi koji predstavljaju preovladavajuće zdravstvene probleme širom svijeta. Sjedeće ponašanje je također povezano s debljanjem i gojaznošću, glavoboljama, povećanim rizikom od kardiovaskularnih bolesti, te može biti i uzrok hroničnih zdravstvenih stanja poput dijabetesa i određenih karcinoma. Ova promjena u načinu života uveliko je utjecala na ljude širom svijeta, te nas je potaknula na razvoj uređaja koji bi pomogao u održanju zdravlja kičme i leđa.

## Spinewise Pametna Stolica

### Opis
Spinewise pametna stolica je uređaj koji prepoznaje vaš način sjedenja, te vas shodno tome upozorava i daje izvještaje o sjedenju. Potrebno je samo da se udobno smjestite u Spinewise pametnu stolicu, prijavite se u aplikaciju i povežete stolicu. Na web stranici možete da vidite:

- **Raspodjelu težine na stolici (Heatmap)**
- **Pregled udjela ispravnog i neispravnog sjedenja**
- **AI odgovor sa podacima i preporukama za sjedenje**
- **Raspodjelu minuta sjedenja po satu**
- **Pregled udjela ispravnog sjedenja po danima**
- **Prikaz količine sjedenja po danima**
- **Pregled stolica i njihovo uređivanje (podešavanje naziva, intervala slanja podataka, kao i podešavanje da li da se podaci uopšte šalju)**
- **Mijenjanje korisničkih informacija**

### Komponente
Spinewise pametna stolica se sastoji od:
- **Stolice**
- **Lilypad TTGO mikrokontrolera**
- **4x senzora pritiska**
- **2x senzora udaljenosti**

### Softver
Za softver smo koristili: 
- **ASP.NET Core Web API**
- **MS SQL Server**
- **Angular**
- **HTML, CSS, JS**


### Stolica i sve komponente
Tokom sjedenja na stolici, mikrokontroler prikuplja podatke od senzora, analizira ih i potom šalje na web server putem WiFi-a. Na serveru se, putem već istreniranog ML algoritma kreiranog putem ML.NET-a, ti podaci o sjedenju klasificiraju kao dobri ili loši, odnosno da li je sjedenje ispravno ili ne. Poslije toga se korisniku automatski na web stranici učitavaju podaci, kao i upozorenje ukoliko korisnik konstantno sjedi neispravno u zadnjih nekoliko minuta.

---

## Tržište
![10](https://github.com/ArminDjidelija/fitcc24-spinewise/assets/110191710/8c4f2ee6-5acd-480e-ae0c-7952d4a77f7b)
## Pismo preporuke od EIT-a (Evropski institut za inovacije i tehnologiju)
![11](https://github.com/ArminDjidelija/fitcc24-spinewise/assets/110191710/6772ab7e-ddb5-456f-8798-eeca0d7869ef)
## Ciljna publika 
![12](https://github.com/ArminDjidelija/fitcc24-spinewise/assets/110191710/5182ed73-fb8c-4178-ace1-9b5a0e504906)
## Spinewise tim
![14](https://github.com/ArminDjidelija/fitcc24-spinewise/assets/110191710/04b21102-f2b7-40a6-a177-3490deb231bd)

# UI
## Homepage
![image](https://github.com/ArminDjidelija/fitcc24-spinewise/assets/110191710/2d93e71b-85d3-4f47-abd0-6403ef8ab84f)
![image](https://github.com/ArminDjidelija/fitcc24-spinewise/assets/110191710/919faa7b-24b0-43dc-903b-bb0429c7ca42)
![image](https://github.com/ArminDjidelija/fitcc24-spinewise/assets/110191710/4f103531-0197-453d-8d7f-797aee2599d7)
## Sign in
![image](https://github.com/ArminDjidelija/fitcc24-spinewise/assets/110191710/27d0f158-09d2-4e85-83ce-f11f2a698cdf)
## User dashboard
![image](https://github.com/ArminDjidelija/fitcc24-spinewise/assets/110191710/d494956f-4766-452e-9ee1-739481765f81)
![image](https://github.com/ArminDjidelija/fitcc24-spinewise/assets/110191710/09fc4972-cdab-4669-a013-b4fe786597e3)
![image](https://github.com/ArminDjidelija/fitcc24-spinewise/assets/110191710/75989ca4-ff87-4723-a312-1463508e8107)
## User chairs
![image](https://github.com/ArminDjidelija/fitcc24-spinewise/assets/110191710/80e244e0-4d23-4622-b139-394cf7b1055e)
## User profile
![image](https://github.com/ArminDjidelija/fitcc24-spinewise/assets/110191710/2adef965-6e49-47d4-b533-eebf3bf2141e)


### Video demonstracija
https://github.com/ArminDjidelija/fitcc24-spinewise/assets/110191710/1031e182-d877-4a2c-9460-2384a5cc3189

