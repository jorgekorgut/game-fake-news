using System.Collections;
using UnityEngine;
using System;
using Platformer.Mechanics;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class SceneHandler : MonoBehaviour
{
    private bool isPlayerStunned = false;
    private float countDown; 
    public PlayerController player;

    public Dictionary <String, Dialog> dialogMap;
    public TMP_Text characterName;
    public TMP_Text content;

    private Dialog currentDialog;
    private int currentDialogIndex;
    private float dialogStepDelay = 0;

    public GameObject oldMan;
    public GameObject oldWoman;
    public GameObject poorMan;
    public GameObject hotDog;
    public GameObject lingeMan;
    public GameObject entrepreneur;
    public GameObject militaire;
    public GameObject evangelique;
    public GameObject runner;
    public GameObject mechanic;
    public GameObject scientist;
    public GameObject student;

    public void initialize()
    {
        countDown = 0;
        dialogStepDelay = 0;
        currentDialogIndex=0;
        currentDialog = null;
        content.text = "";
        characterName.text = "";
        desactivateAllImages();
        if(dialogMap != null)
        {
            foreach(KeyValuePair<String, Dialog> entry in dialogMap)
            {
                entry.Value.colected = false;
            }
        }
    }

    void Awake()
    {
        countDown = dialogStepDelay;

        dialogMap = new Dictionary<String, Dialog>();
        dialogMap["old_man"] = new Dialog(){
            id = "old_man",
            name = "José",
            discution = new string[]{"Bonjour jeune homme, comment ça va ?",
            "As-tu déjà vu le dernier débat présidentiel ?",
            "Notre aimable président vient d'annoncer que la campagne de vaccination contre la COVID-19 du brésil a été la plus efficace du monde.",
            "J’ai hâte de savoir ce qu’il nous garde pour l’avenir.",
            " Pour ça, il faut que le maudit communiste ne gagne pas.",
            "J’espère que tu ne votes pas pour lui."},
            timing = new int[]{4,4,7,4,4,4},
            information = "Le président Bolsonaro(22) vient d'annoncer que la campagne de vaccination contre la COVID-19 du brésil a été la plus efficace du monde.",
            response = 22
        };

        dialogMap["old_woman"] = new Dialog(){
            id = "old_woman",
            name = "Maria",
            discution = new string[]{"Salut mon petit. Est-ce que tu te rends compte des conneries que notre président a dit en diffusion internationale ?",
            "Juste hier il vient de dire que si on prenait le vaccin on deviendrait tous des crocodiles.",
            "C'est une honte pour notre pays."},
            timing = new int[]{8,6,4},
            information = "Le président Bolsonaro(22) vient d'annoncer en diffusion internationale que si on prenait le vaccin on deviendrait tous des crocodiles.",
            response = 13
        };

        dialogMap["poor_man"] = new Dialog(){
            id = "poor_man",
            name = "Joaquim",
            discution = new string[]{"Bonjour petit monsieur, As-tu de l’argent pour le petit pain du soir ?",
            "Depuis que Bolsonaro a changé les règles de la bolsa familia… Désolé Bolsa Brasil,",
            "j’ai perdu mon support financier et donc je ne peux plus payer mon logement ici.",
            "C’est la crise."},
            timing = new int[]{6,6,6,3},
            information = "Le président Bolsonaro(22) renomme le projet d'aide social financière Bolsa familia en Bolsa Brasil et restreint la quantité des personnes éligibles.",
            response = 13
        };

        dialogMap["hot_dog"] = new Dialog(){
            id = "hot_dog",
            name = "Bruna",
            discution = new string[]{"Hey garçon, comme d’habitude ?",
            "Deux saucisses, de la purée et de la mayonnaise ?",
            "Je te fais ça tout de suite.",
            "Voilà, ça sera 12 reais. Peux-tu me payer par PIX ?",
            "Cette merveilleuse invention de paiement sécurisé et instantané que notre adorable président dit avoir créé.",
            "Sans celui-ci je payerais énormément des taxes de transaction aux banques et ça serait impossible de tenir mon stand.",
            "Vive Bolsonaro."},
            timing = new int[]{5,5,4,5,7,7,4},
            information = "Bolsonaro(22) affirme avoir créé le PIX, une forme de paiement sécurisé et instantanée qui réduit les taxes lors de transactions.",
            response = 22
        };

        dialogMap["linge_man"] = new Dialog(){
            id = "linge_man",
            name = "João",
            discution = new string[]{"Salut jeune homme.",
            "Je me disais, il n’y a pas de moyen pour que Bolsonaro gagne ses élections.",
            "Tu savais qu’il a été le candidat le plus voté de l’histoire du Brésil au premier tour ?",
            "Lula lá… Brilhar nossa estrela. Lula lá… Renasce a esperança… Lula lá.",
            "O Brasil criança na alegria de se abraçar…"},
            timing = new int[]{4,6,4,4,3},
            information = "Lula(13) a été le candidat le plus voté au premier tour dans l’histoire du Brésil.",
            response = 13
        };

        dialogMap["entrepreneur"] = new Dialog(){
            id = "entrepreneur",
            name = "Lucas",
            discution = new string[]{"Bonjour monsieur",
            "Qu’est-ce que vous faites à se balader dans les rues de mon quartier à cette heure-ci ?",
            "N'êtes vous pas un voleur par hasard ?",
            "Trouvez-vous de quoi vous occuper la nuit,",
            "sinon vous finirez en prison, ou comme le monsieur là-bas, dans les drogues.",
            "Bolsonaro a fini avec la mamata, maintenant si tu veux de l’argent tu dois travailler pour.",
            "Un vrai système méritocratique est établi. Adieu corruption."},
            timing = new int[]{4,6,5,5,6,7,6},
            information = "Bolsonaro(22) incite une méritocratie ultime et défend être l’homme capable de finir avec toute sorte de corruption existante dans le Brésil.",
            response = 22
        };

        dialogMap["militaire"] = new Dialog(){
            id = "militaire",
            name = "Mateus",
            discution = new string[]{"Aïe Aïe, comment va la jeunesse ?",
            "C’est vous l’avenir.",
            "Il ne faut pas faire confiance aux “capitaines” qui se présentent à la politique.",
            "Est-ce que vous savez que notre président a été viré de l'armée pour avoir menacé l’explosion des casernes militaires?",
            "N’importe quoi ces hommes politiques d’aujourd’hui."},
            timing = new int[]{4,3,5,7,5},
            information = "Bolsonaro(22) a été viré de l'armée pour avoir planifié l’explosion des casernes militaires.",
            response = 13
        };

        dialogMap["evangelique"] = new Dialog(){
            id = "evangelique",
            name = "Tereza",
            discution = new string[]{"Bonjour mon petit.",
            "Tu ne vas pas croire à ce que j’ai vu sur Facebook.",
            "Silas Malafaia a tout dit dans sa vidéo.",
            "Lula ne défend pas la vie.",
            "Comment c’est possible qu’un candidat à la présidence du pays puisse être favorable à un massacre de ceux qui ne peuvent pas se défendre.",
            "Disons non à l’avortement.",
            "C’est vraiment une honte qu’il y ait des gens qui le défendent.",
            "Que Dieu nous préserve de ces horribles créatures."},
            timing = new int[]{4,5,5,4,8,4,6,6},
            information = "En prenant en compte un point de vue concervateur pour répondre à cette question, Lula(13) est favorable à l’avortement.",
            response = 22
        };

        dialogMap["runner"] = new Dialog(){
            id = "runner",
            name = "Sofia",
            discution = new string[]{"Coucou, on part pour une petite course là maintenant ?",
            "Je rigole, je viens de finir 10 km et je suis morte.",
            "Et toi, tu votes pour qui demain ?",
            "J'espère que ne soit pas pour Bolsonaro.",
            "Il vient de pousser à 2027 la loi de soutien au sport.",
            "La loi qui aurait pu faire développer énormément le sport dans notre pays et découvrir des nouveaux talents.",
            "Vraiment dommage."},
            timing = new int[]{5,5,5,5,5,8,4},
            information = "Le vote de la loi de soutien au sport a été repoussé jusqu'en 2027 par Bolsonaro(22).",
            response = 13
        };

        dialogMap["mechanic"] = new Dialog(){
            id = "mechanic",
            name = "Airton",
            discution = new string[]{"Salut mec. Ça-va ?",
            "Par chance, avec l’augmentation du SMIC, je peux finalement commencer à investir dans mon avenir.",
            "Je travaille comme mécanicien ici le matin et je pars à l’université le soir.",
            "Je vais devenir un boss de l’informatique.",
            "Tu arrives à croire ?",
            "Tout ça grâce à 75 % d’augmentation pendant le mandat d'un seul parti politique.",
            "Vraiment incroyable ce que Lula et Dilma ont fait pour le Brésil.",
            "Vive les vrais présidents."},
            timing = new int[]{4,8,7,5,4,7,7,4},
            information = "Pendant les mandats de Lula(13) et Dilma, deux politiciens du PT, un parti politique de gauche, le brésil à expérimenté une augmentation de 75% du SMIC.",
            response = 13
        };

        dialogMap["scientist"] = new Dialog(){
            id = "scientist",
            name = "Rafael",
            discution = new string[]{"Ah Bonjour, je ne vous avais pas vu.",
            "Comment allez-vous ?",
            "Je suis ici en train de négocier avec mon université pour avoir une subvention sur mon projet scientifique.",
            "Cependant ce n’est pas facile.",
            "Depuis que Bolsonaro a coupé l’investissement dans l’éducation, c’est très difficile de réussir à avoir n’importe quelle type de financement.",
            "Effectivement avec le moins d’argent mis dans l’éducation depuis les années 2000, il ne fallait pas s’attendre à une autre chose de son mandat."},
             timing = new int[]{5,4,8,5,12,12},
            information = "Pendant le mandat de Bolsonaro(22), le Brésil reste avec un investissement dans l’éducation très bas, ce qui se compare avec celui des années 2000.",
            response = 13
        };

        dialogMap["student"] = new Dialog(){
            id = "student",
            name = "Guilherme",
            discution = new string[]{"Salut.",
            "Grâce à Lula, en 2015, j’ai pu intégrer mon université ici à Curitiba.",
            "Avec les aides sociales pour mon logement et la nourriture je peux même me permettre de m’investir totalement dans mes études.",
            "C’est incroyable que 8 millions d’étudiants ont été inscrits dans un enseignement supérieur pendant 2015.",
            "Une vraie chance d’avoir un président qui priorise l’éducation."},
            timing = new int[]{3,5,12,10,7},
            information = "En 2015, pendant le deuxième mandat du PT, parti politique de Lula(13), le Brésil avait 8 millions d’universitaires inscrits dans une institution d’enseignement supérieur.",
            response = 13
        };
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayerStunned)
        {
            player.controlEnabled = false;
            if(countDown>0 && !Input.GetButtonDown("Cancel"))     
            {         
                countDown -= Time.deltaTime;
            }
            else
            {
                if(currentDialogIndex < currentDialog.discution.Length)
                {
                    content.text = currentDialog.discution[currentDialogIndex];
                    dialogStepDelay = currentDialog.timing[currentDialogIndex];
                    currentDialogIndex++;
                }
                else
                {
                    isPlayerStunned = false;
                    player.controlEnabled = true;
                    this.gameObject.SetActive(false);
                    dialogStepDelay = 0;
                }
                countDown = dialogStepDelay;
            }    
        }
        
    }

    public void displayDialog(String id)
    {
        this.gameObject.SetActive(true);
        isPlayerStunned = true;
        desactivateAllImages();
        currentDialog = dialogMap[id];
        
        characterName.text = currentDialog.name;
        currentDialogIndex = 0;
        switch(id)
        {
            case "old_man":
                oldMan.SetActive(true);
                break;
            case "old_woman":
                oldWoman.SetActive(true);
                break;
            case "poor_man":
                poorMan.SetActive(true);
                break;
            case "hot_dog":
                hotDog.SetActive(true);
                break;
            case "entrepreneur":
                entrepreneur.SetActive(true);
                break;
            case "linge_man":
                lingeMan.SetActive(true);
                break;
            case "militaire":
                militaire.SetActive(true);
                break;
            case "runner":
                runner.SetActive(true);
                break;
            case "evangelique":
                evangelique.SetActive(true);
                break;
            case "mechanic":
                mechanic.SetActive(true);
                break;
            case "scientist":
                scientist.SetActive(true);
                break;
            case "student":
                student.SetActive(true);
                break;
        }
        currentDialog.colected = true;
    }

    public void hideDialog()
    {
        this.gameObject.SetActive(false);
    }

    private void desactivateAllImages()
    {
        oldMan.SetActive(false);
        oldWoman.SetActive(false);
        poorMan.SetActive(false);
        hotDog.SetActive(false);
        entrepreneur.SetActive(false);
        lingeMan.SetActive(false);
        militaire.SetActive(false);
        evangelique.SetActive(false);
        runner.SetActive(false);
        mechanic.SetActive(false);
        scientist.SetActive(false);
        student.SetActive(false);
    }
}
