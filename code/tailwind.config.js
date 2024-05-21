/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./**/*.{html,cshtml,razor,cs,js}"],
  theme: {
    extend: {
      fontFamily: {
        'kanit': ['Kanit', 'sans-serif'],
        'archivo': ['Archivo', 'sans-serif']
      },
      colors: {
        'accent': 'hsl(235 45% 53%)',
        'accent-hover': 'hsl(235 84% 66%)',
        'invalid': 'hsl(348 100% 58%)',
        'muted-foreground': 'hsl(230 12% 55%)',
        'foreground': 'hsl(230 47% 98%)',
        'background': 'hsl(231 19% 11%)',
        'background-accent': 'hsl(230 22% 16%)',
      },
    },
    fontSize: {
      xs: '0.7rem',
      sm: '0.9rem',
      md: '1rem',
      lg: '1.1rem',
      'xl': '1.563rem',
      '2xl': '1.953rem',
      '3xl': '2.441rem',
      '4xl': '3.052rem',
    },
  },
  plugins: [],
}

