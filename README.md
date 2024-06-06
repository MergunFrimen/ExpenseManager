# Expense Manager

## Requirements

stejne jako v interaktivni osnove predmetu:

- [x] zaregistrovat se - heslo nesmí být uloženo v plain textu
- [x] přihlášení/odhlášení
- [x] přidat výdaj/příjem
- [ ] každý výdaj/příjem klasifikovat do několik předpřipravených kategorií
- [ ] zobrazení aktuálního stavu účtu uživatele
- [ ] přidat nové kategorie pro klasifikaci příjmu a výdajů
- [ ] filtrování podle více parametrů (kategorie, měsíc, rok…)
- [x] CRUD tam, kde to dává smysl
- [ ] asynchronně importovat a exportovat výdaje/příjmy
- [ ] zobrazit nějaké jednoduché statistiky a exportovat je do grafu (v případě aplikace s GUI stačí tento graf zobrazit)


navic:

- [x] bude to webova aplikace
- [x] backend v C# (WebAPI etc.) a minimalni frontend (nejspis React nebo Solidjs)
- [x] duraz na clean architecture a event driven architekturu (MediatR)
- [ ] deployment pres Docker container
- [ ] pokud budu mit cas tak to dam i na Azure pres Terraform

## How to run

### Docker

```bash
docker-compose up
```

### Local

Run the web API:
```bash
dotnet build
dotnet run
```

Run the frontend:
```bash
cd ExpenseManager.Presentation/Client
npm i
npm run dev
```

## Sources

- [Lukáš Grolig](https://www.youtube.com/watch?v=E7vQ8Rvq2rw)

- [Amichai Mantinband](https://www.youtube.com/@amantinband/)

- [Jason Taylor](https://www.youtube.com/watch?v=dK4Yb6-LxAk)

- [Remigiusz Zalewski](https://www.youtube.com/@remigiuszzalewski)
