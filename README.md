# Bayaan – A Pakistani Poetry Platform  

**Bayaan** is a modern, digital platform designed to preserve and celebrate Pakistani poetry. Inspired by Rekhta, Bayaan provides an online space where poets and poetry lovers can connect, share, and explore the richness of Urdu literature.  

Built with a **Blazor frontend** and a **SQL-backed n8n-powered backend**, Bayaan is part of my Final Year Project (FYP) to combine technology with cultural heritage.  

---

## 🌟 Key Features  

- **Poet Profiles**  
  Poets can create and manage their profiles, each linking directly to their published works.  

- **Poetry Submission System**  
  Structured JSON-based submission for Ghazals and Nazms, with admin approval for authenticity.  

- **Search & Discovery**  
  Users can search poets by name or nickname, and explore poetry collections in clean, scannable layouts.  

- **Community Features**  
  Testimonials and feedback submission with moderation.  
  Future analytics support for trending poetry themes.  

---

## 🛠️ Tech Stack  

- **Frontend:** Blazor (C#)  
- **Backend:** SQL Server + Stored Procedures  
- **Architecture:** Layered (DAL, Service Layer, Entities)  
- **Workflow & Automation:** n8n (for content approval, moderation, automation)  
- **Authentication:** Google login (claims-based identity)  
- **APIs:** Custom endpoints for poetry submission & retrieval  

---

## 🚀 Workflow Overview  

1. **User Login** → via Google or form-based authentication  
2. **Become a Poet** → Submit poet profile request  
3. **Admin Approval** → `SP_InsertPoet` stored procedure updates poet role  
4. **Submit Poetry** → Ghazal/Nazm submitted in structured JSON, stored in DB  
5. **Public Display** → Clean Blazor pages render poetry dynamically  

---

## 🎯 Project Goals  

- Preserve Pakistani poetry in a modern, accessible platform.  
- Create a bridge between traditional literature and digital culture.  
- Provide an engaging, ATS-friendly way for poetry enthusiasts and writers to showcase their work.  

---

## 🔮 Future Enhancements  

- Advanced search by poetry themes, keywords, and emotions.  
- AI-powered poetry recommendations.  
- Mobile app integration for wider access.  

---

## 📖 About Bayaan  

**Bayaan** means *expression* — this project is dedicated to giving poets a digital voice while keeping the charm of Urdu poetry alive in the modern age.  
